﻿/*----------------------------------------------------------------
    Copyright (C) 2021 Senparc
    
    文件名：ExternalApi.cs
    文件功能描述：外部联系人接口
    
    
    创建标识：Senparc - 20181009
   
    修改标识：lishewen - 20200318
    修改描述：v3.7.401 新增“获取客户群列表”“获取客户群详情” API
    
    修改标识：gokeiyou - 20201013
    修改描述：v3.7.604 添加外部联系人管理 > 客户管理相关接口
    
    修改标识：Senparc - 20210321
    修改描述：v3.8.201 添加“配置客户联系「联系我」方式”接口
    
    修改标识：Senparc - 20210321
    修改描述：v3.9.101 添加“获取配置了客户联系功能的成员列表”接口

    修改标识：WangDrama - 20210630
    修改描述：v3.9.600 添加：外部联系人 - 客户群统计+联系客户+群直播+客户群事件 相关功能

    修改标识：WangDrama - 20210714
    修改描述：v3.11-preview1

    修改标识：WangDrama - 20210807
    修改描述：v3.12.1 添加企业微信入群欢迎语素材

----------------------------------------------------------------*/

/*
    官方文档：https://work.weixin.qq.com/api/doc#13473
 */

using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.External;
using Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 外部联系人管理
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
    public static class ExternalApi
    {
        #region 同步方法

        /// <summary>
        /// 离职成员的外部联系人再分配
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="ExternalUserId"></param>
        /// <param name="handoverUserId"></param>
        /// <param name="takeoverUserId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult TransferExternal(string accessTokenOrAppKey, string ExternalUserId, string handoverUserId, string takeoverUserId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/crm/transfer_external_contact?access_token={0}", accessToken);
                var data = new
                {
                    external_userid = ExternalUserId,
                    handover_userid = handoverUserId,
                    takeover_userid = takeoverUserId
                };
                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);

        }

        /// <summary>
        /// 获取外部联系人详情
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="ExternalUserId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetExternalContactResultJson GetExternalContact(string accessTokenOrAppKey, string ExternalUserId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/crm/get_external_contact?access_token={0}&external_userid={1}", accessToken, ExternalUserId);

                return CommonJsonSend.Send<GetExternalContactResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, accessTokenOrAppKey);

        }
        /// <summary>
        /// 获取客户群列表
        /// 权限说明:
        /// 企业需要使用“客户联系”secret或配置到“可调用应用”列表中的自建应用secret所获取的accesstoken来调用。
        /// 暂不支持第三方调用。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="data">查询参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GroupChatListResult GroupChatList(string accessTokenOrAppKey, GroupChatListParam data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/groupchat/list?access_token={0}", accessToken);
                return CommonJsonSend.Send<GroupChatListResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);

        }
        /// <summary>
        /// 获取客户群详情
        /// 权限说明:
        /// 企业需要使用“客户联系”secret或配置到“可调用应用”列表中的自建应用secret所获取的accesstoken来调用。
        /// 暂不支持第三方调用。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="chat_id">客户群ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GroupChatGetResult GroupChatGet(string accessTokenOrAppKey, string chat_id, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/groupchat/get?access_token={0}", accessToken);
                var data = new
                {
                    chat_id
                };
                return CommonJsonSend.Send<GroupChatGetResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);

        }

        #region 客户管理

        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="userid">企业成员的userid</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetExternalContactListResult GetExternalContactList(string accessTokenOrAppKey, string userid, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/list?access_token={accessToken}&userid={userid}";

                return CommonJsonSend.Send<GetExternalContactListResult>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 获取客户详情
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="externalUserId">外部联系人的userid，注意不是企业成员的帐号</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetExternalContactResultJson GetExternalContactInfo(string accessTokenOrAppKey, string externalUserId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/get?access_token={accessToken}&external_userid={externalUserId}";

                return CommonJsonSend.Send<GetExternalContactResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 批量获取客户详情
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="userid"></param>
        /// <param name="cursor"></param>
        /// <param name="limit"></param>
        /// <param name="timeOut"></param>
        public static GetExternalContactInfoBatchResult GetExternalContactInfoBatch(string accessTokenOrAppKey, string userid, string cursor = "", int limit = 50, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/batch/get_by_user?access_token={accessToken}";

                var data = new
                {
                    userid,
                    cursor,
                    limit
                };

                return CommonJsonSend.Send<GetExternalContactInfoBatchResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 修改客户备注信息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="rquest">请求报文</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult UpdateExternalContactRemark(string accessTokenOrAppKey, UpdateExternalContactRemarkRequest rquest, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/remark?access_token={accessToken}";

                return CommonJsonSend.Send<WorkJsonResult>(null, url, rquest, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }


        /// <summary>
        /// 获取配置了客户联系功能的成员列表
        /// <para>权限说明：</para>
        /// <para>企业需要使用“客户联系”secret或配置到“可调用应用”列表中的自建应用secret所获取的accesstoken来调用</para>
        /// <para>第三方应用需具有“企业客户权限->客户基础信息”权限</para>
        /// <para>第三方/自建应用只能获取到可见范围内的配置了客户联系功能的成员。</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult GetFollowUserList(string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/get_follow_user_list?access_token={accessToken}";

                return CommonJsonSend.Send<WorkJsonResult>(null, url, null, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        #region 「联系我」

        /// <summary>
        /// 配置客户联系「联系我」方式
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="rquest">请求报文</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static AddContactWayResult AddContactWay(string accessTokenOrAppKey, AddContactWayRequest rquest, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/add_contact_way?access_token={accessToken}";

                return CommonJsonSend.Send<AddContactWayResult>(null, url, rquest, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        #endregion


        #endregion

        #region 统计管理
        /// <summary>
        /// 获取「联系客户统计」数据
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetUserBehaviorDataListResult GetUserBehaviorData(string accessTokenOrAppKey, GetUserBehaviorDataParam data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_user_behavior_data?access_token={0}", accessToken);
                return CommonJsonSend.Send<GetUserBehaviorDataListResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);

        }
        /// <summary>
        /// 获取「群聊数据统计」数据 按群主聚合的方式
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetGroupChatListResult GroupChatStatisticOwner(string accessTokenOrAppKey, GetGroupChatParam data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/groupchat/statistic?access_token={0}", accessToken);
                return CommonJsonSend.Send<GetGroupChatListResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);

        }


        /// <summary>
        /// 获取「群聊数据统计」数据 按自然日聚合的方式
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetGroupChatGroupByDayListResult GroupChatStatisticGroupByDay(string accessTokenOrAppKey, GetGroupChatGroupByDayParam data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/groupchat/statistic_group_by_day?access_token={0}", accessToken);
                return CommonJsonSend.Send<GetGroupChatGroupByDayListResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        #endregion

        #region 客户标签管理

        #region 管理企业标签



        #endregion

        #region 编辑客户企业标签



        #endregion


        #endregion

        #region 朋友圈

        /// <summary>
        /// 获取企业全部的发表内容。
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetMomentListResult GetMomentList(string accessTokenOrAppKey, GetMomentListParam data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_moment_list?access_token={0}", accessToken);
                return CommonJsonSend.Send<GetMomentListResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 获取企业发表的朋友圈成员执行情况
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetMomentTaskResult GetMomentTask(string accessTokenOrAppKey, GetMomentTaskParam data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_moment_task?access_token={0}", accessToken);
                return CommonJsonSend.Send<GetMomentTaskResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        #endregion

        #region 入群欢迎语
        /// <summary>
        /// 添加入群欢迎语素材
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GroupWelcomeTemplateAddResult GroupWelcomeTemplateAdd(string accessTokenOrAppKey, GroupWelcomeTemplateAddRequest data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/group_welcome_template/add?access_token={0}", accessToken);
                return CommonJsonSend.Send<GroupWelcomeTemplateAddResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 编辑入群欢迎语素材
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult GroupWelcomeTemplateEdit(string accessTokenOrAppKey, GroupWelcomeTemplateEditRequest data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/group_welcome_template/edit?access_token={0}", accessToken);
                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 获取入群欢迎语素材
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="template_id"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GroupWelcomeTemplateGetResult GroupWelcomeTemplateGet(string accessTokenOrAppKey, string template_id, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    template_id = template_id
                };
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/group_welcome_template/get?access_token={0}", accessToken);
                return CommonJsonSend.Send<GroupWelcomeTemplateGetResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 删除入群欢迎语素材
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="template_id"></param>
        /// <param name="agentid"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult GroupWelcomeTemplateDel(string accessTokenOrAppKey, string template_id, long? agentid, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    template_id = template_id,
                    agentid = agentid
                };
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/group_welcome_template/del?access_token={0}", accessToken);
                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }
        #endregion

        #endregion


        #region 异步方法

        /// <summary>
        /// 【异步方法】离职成员的外部联系人再分配
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="ExternalUserId"></param>
        /// <param name="handoverUserId"></param>
        /// <param name="takeoverUserId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetFollowUserListResult> TransferExternalAsync(string accessTokenOrAppKey, string ExternalUserId, string handoverUserId, string takeoverUserId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/crm/transfer_external_contact?access_token={0}", accessToken);
                var data = new
                {
                    external_userid = ExternalUserId,
                    handover_userid = handoverUserId,
                    takeover_userid = takeoverUserId
                };
                return await CommonJsonSend.SendAsync<GetFollowUserListResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);

        }

        /// <summary>
        /// 【异步方法】获取外部联系人详情
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="ExternalUserId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetExternalContactResultJson> GetExternalContactAsync(string accessTokenOrAppKey, string ExternalUserId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/crm/get_external_contact?access_token={0}&external_userid={1}", accessToken, ExternalUserId);

                return await CommonJsonSend.SendAsync<GetExternalContactResultJson>(null, url, null, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);

        }
        /// <summary>
        /// 【异步方法】获取客户群列表
        /// 权限说明:
        /// 企业需要使用“客户联系”secret或配置到“可调用应用”列表中的自建应用secret所获取的accesstoken来调用（accesstoken如何获取？）。
        /// 暂不支持第三方调用。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="data">查询参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GroupChatListResult> GroupChatListAsync(string accessTokenOrAppKey, GroupChatListParam data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/groupchat/list?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<GroupChatListResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);

        }
        /// <summary>
        /// 【异步方法】获取客户群详情
        /// 权限说明:
        /// 企业需要使用“客户联系”secret或配置到“可调用应用”列表中的自建应用secret所获取的accesstoken来调用。
        /// 暂不支持第三方调用。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="chat_id">客户群ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GroupChatGetResult> GroupChatGetAsync(string accessTokenOrAppKey, string chat_id, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/groupchat/get?access_token={0}", accessToken);
                var data = new
                {
                    chat_id
                };
                return await CommonJsonSend.SendAsync<GroupChatGetResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);

        }

        #region 客户管理

        /// <summary>
        /// 【异步方法】获取客户列表
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="userid">企业成员的userid</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetExternalContactListResult> GetExternalContactListAsync(string accessTokenOrAppKey, string userid, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/list?access_token={accessToken}&userid={userid}";

                return await CommonJsonSend.SendAsync<GetExternalContactListResult>(null, url, null, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取客户详情
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="externalUserId">外部联系人的userid，注意不是企业成员的帐号</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetExternalContactResultJson> GetExternalContactInfoAsync(string accessTokenOrAppKey, string externalUserId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/get?access_token={accessToken}&external_userid={externalUserId}";

                return await CommonJsonSend.SendAsync<GetExternalContactResultJson>(null, url, null, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】批量获取客户详情
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="userid"></param>
        /// <param name="cursor"></param>
        /// <param name="limit"></param>
        /// <param name="timeOut"></param>
        public static async Task<GetExternalContactInfoBatchResult> GetExternalContactInfoBatchAsync(string accessTokenOrAppKey, string userid, string cursor = "", int limit = 50, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/batch/get_by_user?access_token={accessToken}";

                var data = new
                {
                    userid,
                    cursor,
                    limit
                };

                return await CommonJsonSend.SendAsync<GetExternalContactInfoBatchResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 修改客户备注信息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="rquest">请求报文</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> UpdateExternalContactRemarkAsync(string accessTokenOrAppKey, UpdateExternalContactRemarkRequest rquest, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/remark?access_token={accessToken}";

                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, rquest, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】获取配置了客户联系功能的成员列表
        /// <para>权限说明：</para>
        /// <para>企业需要使用“客户联系”secret或配置到“可调用应用”列表中的自建应用secret所获取的accesstoken来调用</para>
        /// <para>第三方应用需具有“企业客户权限->客户基础信息”权限</para>
        /// <para>第三方/自建应用只能获取到可见范围内的配置了客户联系功能的成员。</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetFollowUserListResult> GetFollowUserListAsync(string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/get_follow_user_list?access_token={accessToken}";

                return await CommonJsonSend.SendAsync<GetFollowUserListResult>(null, url, null, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey);
        }

        #endregion

        #region 统计管理
        /// <summary>
        /// 获取「联系客户统计」数据
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetUserBehaviorDataListResult> GetUserBehaviorDataAsync(string accessTokenOrAppKey, GetUserBehaviorDataParam data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_user_behavior_data?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<GetUserBehaviorDataListResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);

        }


        /// <summary>
        /// 获取「群聊数据统计」数据 按群主聚合的方式
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetGroupChatListResult> GroupChatStatisticOwnerAsync(string accessTokenOrAppKey, GetGroupChatParam data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/groupchat/statistic?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<GetGroupChatListResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);

        }
        /// <summary>
        /// 获取「群聊数据统计」数据 按自然日聚合的方式
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetGroupChatGroupByDayListResult> GroupChatStatisticGroupByDayAsync(string accessTokenOrAppKey, GetGroupChatGroupByDayParam data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/groupchat/statistic_group_by_day?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<GetGroupChatGroupByDayListResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        #endregion

        #region 朋友圈

        /// <summary>
        /// 获取企业全部的发表内容。
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetMomentListResult> GetMomentListAsync(string accessTokenOrAppKey, GetMomentListParam data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_moment_list?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<GetMomentListResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }
        /// <summary>
        /// 获取企业发表的朋友圈成员执行情况
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetMomentTaskResult> GetMomentTaskAsync(string accessTokenOrAppKey, GetMomentTaskParam data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_moment_task?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<GetMomentTaskResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        #endregion

        #region 入群欢迎语
        /// <summary>
        /// 添加入群欢迎语素材
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GroupWelcomeTemplateAddResult> GroupWelcomeTemplateAddAsync(string accessTokenOrAppKey, GroupWelcomeTemplateAddRequest data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/group_welcome_template/add?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<GroupWelcomeTemplateAddResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }
        /// <summary>
        /// 编辑入群欢迎语素材
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> GroupWelcomeTemplateEditAsync(string accessTokenOrAppKey, GroupWelcomeTemplateEditRequest data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/group_welcome_template/edit?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取入群欢迎语素材
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="template_id"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GroupWelcomeTemplateGetResult> GroupWelcomeTemplateGetAsync(string accessTokenOrAppKey, string template_id, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    template_id = template_id
                };
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/group_welcome_template/get?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<GroupWelcomeTemplateGetResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 删除入群欢迎语素材
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="template_id"></param>
        /// <param name="agentid"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> GroupWelcomeTemplateDelAsync(string accessTokenOrAppKey, string template_id, long? agentid, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    template_id = template_id,
                    agentid = agentid
                };
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/group_welcome_template/del?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }
        #endregion

        #region 「联系我」

        /// <summary>
        /// 配置客户联系「联系我」方式
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="rquest">请求报文</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<AddContactWayResult> AddContactWayAsync(string accessTokenOrAppKey, AddContactWayRequest rquest, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/add_contact_way?access_token={accessToken}";

                return await CommonJsonSend.SendAsync<AddContactWayResult>(null, url, rquest, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey);
        }

        #endregion


        #endregion
    }
}
