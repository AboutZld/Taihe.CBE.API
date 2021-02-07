using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiheSystem.CBE.Api.GWF;
using TaiheSystem.CBE.Api.Model;
using SqlSugar;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace TaiheSystem.CBE.Api.GWF
{
    public class Step
    {
        public const string DIRTY_DATA_PROMPT = "正在操作的数据可能已被其他用户操作, 请重新读取数据!";
        public const string Step_Insert_Sql = @"insert into gwf_Step(FlowCode,OperationCode,Node_From,Node_To,StepRemark,CreateTime,CreateID,CreateName,{0})
values (@FlowCode,@OperationCode,@Node_From,@Node_To,@StepRemark,getdate(),@UserID,@UserName,'{1}')";



        /// <summary>
        /// 提交模块
        /// </summary>
        /// <param name="db">数据库连接db</param>
        /// <param name="xeBizEntity">提交结构</param>
        /// <param name="KeyIDValue">主键值</param>
        /// <param name="StatusFrom">流程起始值</param>
        /// <param name="OperactionCode">流程编码</param>
        /// <param name="updateBizEntity">后置执行</param>
        /// <param name="StepRemark">提交说明</param>
        public static void Submit(SqlSugarClient db,object xeBizEntity,string tableName, string KeyIDValue,string StatusFrom,string OperactionCode,List<SugarParameter> parameters,Action<SqlSugarClient,List<SugarParameter>> updateBizEntity,string StepRemark = "",bool isExecuteAfterDone = true)
        {
            JObject jBizEntity = Common.Helpers.JsonHelpers.DeserializeJson<JObject>(Common.Helpers.JsonHelpers.SerializeJson(xeBizEntity));
            if (!Operation.OperationList.Any(m => m.OperationCode == OperactionCode && m.Node_From == jBizEntity[StatusFrom].ToString()))
            {
                throw new Exception("当前流程配置不存在");
            }
            var operation = Operation.OperationList.First(m => m.OperationCode == OperactionCode && m.Node_From== jBizEntity[StatusFrom].ToString());
            
            parameters.Add(new SugarParameter("FlowCode", operation.FlowCode));
            parameters.Add(new SugarParameter("OperationCode", operation.OperationCode));
            parameters.Add(new SugarParameter("Node_From", operation.Node_From));
            parameters.Add(new SugarParameter("Node_To", operation.Node_To));
            parameters.Add(new SugarParameter(tableName + "_" + KeyIDValue, jBizEntity[KeyIDValue].ToString()));
            parameters.Add(new SugarParameter("StepRemark", StepRemark));

            if (updateBizEntity != null)
            {
                updateBizEntity(db, parameters);
            }
            //记录流程步骤(如果前后状态都一致, 就不用记录日志，比如报验登记和报验修改用的都是一个Operation，都不用记录日志)
            if (operation.Node_From != operation.Node_To)
            {
                db.Ado.ExecuteCommand(string.Format(Step_Insert_Sql, tableName + "_" + KeyIDValue, jBizEntity[KeyIDValue].ToString()), parameters);
            }

            //后置操作
            if (isExecuteAfterDone && !string.IsNullOrEmpty(operation.AfterDone))
            {
                string[] afterDones = operation.AfterDone.Split(',');
                foreach (string afterDone in afterDones)
                {
                    MethodInfo afterDoneFunc = GWF.DicAfterDone[afterDone.Trim()];
                    afterDoneFunc.Invoke(new AfterDone(db, xeBizEntity), null);
                }
            }
        }


        public static void Cancel(SqlSugarClient db, object xeBizEntity, string tableName, string KeyIDValue, string StatusFrom, string OperactionCode, List<SugarParameter> parameters, Action<SqlSugarClient, List<SugarParameter>> updateBizEntity, string StepRemark = "", bool isExecuteAfterDone = true)
        {
            JObject jBizEntity = Common.Helpers.JsonHelpers.DeserializeJson<JObject>(Common.Helpers.JsonHelpers.SerializeJson(xeBizEntity));

            var oldstep = db.Ado.SqlQuerySingle<Gwf_Step>(string.Format("select * from gwf_Step where OperationCode = {0} and Node_To = {1} and {2} = '{3}'",OperactionCode,jBizEntity[StatusFrom].ToString(), tableName + "_" + KeyIDValue, jBizEntity[KeyIDValue].ToString()));
            if(oldstep == null)
            {
                throw new Exception("所撤销的提交流程不存在，请核对");
            }

            if (!Operation.OperationList.Any(m => m.OperationCode == OperactionCode && m.Node_To == jBizEntity[StatusFrom].ToString() && m.Node_From == oldstep.Node_From))
            {
                throw new Exception("当前流程配置不存在");
            }
            var operation = Operation.OperationList.First(m => m.OperationCode == OperactionCode && m.Node_To == jBizEntity[StatusFrom].ToString());

            parameters.Add(new SugarParameter("FlowCode", operation.FlowCode));
            parameters.Add(new SugarParameter("OperationCode", operation.OperationCode));
            //撤销操作
            parameters.Add(new SugarParameter("Node_From", operation.Node_To));
            parameters.Add(new SugarParameter("Node_To", operation.Node_From));
            parameters.Add(new SugarParameter(tableName + "_" + KeyIDValue, jBizEntity[KeyIDValue].ToString()));
            parameters.Add(new SugarParameter("StepRemark", StepRemark));

            if (updateBizEntity != null)
            {
                updateBizEntity(db, parameters);
            }
            //记录流程步骤(如果前后状态都一致, 就不用记录日志，比如报验登记和报验修改用的都是一个Operation，都不用记录日志)
            if (operation.Node_From != operation.Node_To)
            {
                db.Ado.ExecuteCommand(string.Format(Step_Insert_Sql, tableName + "_" + KeyIDValue, jBizEntity[KeyIDValue].ToString()), parameters);
            }

            //后置操作
            if (isExecuteAfterDone && !string.IsNullOrEmpty(operation.AfterDone))
            {
                string[] afterDones = operation.AfterDone.Split(',');
                foreach (string afterDone in afterDones)
                {
                    MethodInfo afterDoneFunc = GWF.DicAfterDone[afterDone.Trim()];
                    afterDoneFunc.Invoke(new AfterDone(db, xeBizEntity), null);
                }
            }
        }
    }
}
