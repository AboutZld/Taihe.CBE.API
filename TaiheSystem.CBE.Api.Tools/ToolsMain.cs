using TaiheSystem.CBE.Api.Interfaces;
using System;
using System.IO;

namespace TaiheSystem.CBE.Api.Tools
{
    class ToolsMain
    {

        static void Main(string[] args)
        {

            var allTables = new ToolsService().GetAllTables();
            foreach (var table in allTables)
            {
                if (table == "Sys_User_Classification")
                {
                    Console.Write($"生成[{ table }]表 模型: ");
                    Console.WriteLine(new ToolsService().CreateModels("..\\..\\..\\..\\TaiheSystem.CBE.Api.Model\\Entity", "TaiheSystem.CBE.Api.Model", table, ""));
                    Console.Write($"生成[{ table }]表 服务: ");
                    Console.WriteLine(new ToolsService().CreateServices("..\\..\\..\\..\\TaiheSystem.CBE.Api.Interfaces\\Service", "TaiheSystem.CBE.Api.Interfaces", table));
                    Console.Write($"生成[{ table }]表 接口: ");
                    Console.WriteLine(new ToolsService().CreateIServices("..\\..\\..\\..\\TaiheSystem.CBE.Api.Interfaces\\IService", "TaiheSystem.CBE.Api.Interfaces", table));
                }
            }

            //var allVM = new ToolsService().GetAllViews("view_PlanVM");
            //foreach (var view in allVM)
            //{
            //    Console.Write($"生成[{ view }]表 模型: ");
            //    Console.WriteLine(new ToolsService().CreateModels("..\\..\\..\\..\\TaiheSystem.CBE.Api.Model\\View\\AuditPlan", "TaiheSystem.CBE.Api.Model", view, ""));
            //    Console.Write($"生成[{ view }]表 服务: ");
            //    Console.WriteLine(new ToolsService().CreateServices("..\\..\\..\\..\\TaiheSystem.CBE.Api.Interfaces\\Service", "TaiheSystem.CBE.Api.Interfaces", view));
            //    Console.Write($"生成[{ view }]表 接口: ");
            //    Console.WriteLine(new ToolsService().CreateIServices("..\\..\\..\\..\\TaiheSystem.CBE.Api.Interfaces\\IService", "TaiheSystem.CBE.Api.Interfaces", view));
            //}

            Console.Write($"生成DbContext: ");
            Console.WriteLine(new ToolsService().CreateDbContext("..\\..\\..\\..\\TaiheSystem.CBE.Api.Core\\DbContext.cs", "TaiheSystem.CBE.Api.Core"));
            Console.ReadKey();
        }
    }
}
