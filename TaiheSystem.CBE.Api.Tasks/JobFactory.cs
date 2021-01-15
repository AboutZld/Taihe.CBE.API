﻿/*
* ==============================================================================
*
* FileName: JobFactory.cs
* Created: 2020/6/18 15:37:32
* Author: Taihe
* Description: 
*
* ==============================================================================
*/
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaiheSystem.CBE.Api.Tasks
{
    public class JobFactory : IJobFactory
    {
        /// <summary>
        /// 注入反射获取依赖对象
        /// </summary>
        private readonly IServiceProvider _serviceProvider;
        public JobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 实现接口Job
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="scheduler"></param>
        /// <returns></returns>
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                var serviceScope = _serviceProvider.CreateScope();
                var job = serviceScope.ServiceProvider.GetService(bundle.JobDetail.JobType) as IJob;
                return job;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }

        }
    }
}
