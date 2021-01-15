﻿/*
* ==============================================================================
*
* FileName: ITaskSchedulerServer.cs
* Created: 2020/6/17 13:43:08
* Author: Taihe
* Description: 
*
* ==============================================================================
*/
using TaiheSystem.CBE.Api.Model;
using System.Threading.Tasks;

namespace TaiheSystem.CBE.Api.Tasks
{
    public interface ITaskSchedulerServer
	{
		Task<ApiResult<string>> StartTaskScheduleAsync();

		Task<ApiResult<string>> StopTaskScheduleAsync();

		Task<ApiResult<string>> AddTaskScheduleAsync(Sys_TasksQz tasksQz);

		Task<ApiResult<string>> PauseTaskScheduleAsync(Sys_TasksQz tasksQz);

		Task<ApiResult<string>> ResumeTaskScheduleAsync(Sys_TasksQz tasksQz);

		Task<ApiResult<string>> DeleteTaskScheduleAsync(Sys_TasksQz tasksQz);
	}
}
