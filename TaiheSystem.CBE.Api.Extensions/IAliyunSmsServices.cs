using Aliyun.Acs.Core;
using TaiheSystem.CBE.Api.Extensions.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaiheSystem.CBE.Api.Extensions
{
    public interface IAliyunSmsServices
    {
        CommonResponse TemplateSmsSend(TemplateSmsSendDto parm);
    }
}
