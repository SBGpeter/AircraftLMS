using Best.HTTP;
using Best.HTTP.Request.Upload.Forms;
using System.Collections.Generic;
using UnityEngine;
using System;
using Best.HTTP.Cookies;

namespace X2R.HTTP
{
    [System.Serializable]
    public class Login
    {
        public long user_service_provider_id;//": 1,
        public int user_role_code;//": 40,
        public string user_id;//": "student01",
        public int is_certified;//": 0,
        public string session_key;//": "c2c48fb2d548486fa1c04537b9f1552f",
        public int user_device_type_code;//": 2,
        public int need_to_change_user_password;//: 0,
        public int need_to_update_user_profile;//: 0,
        public int user_idx;//": 3,
        public int user_status_code;//": 1,
        public string user_password_last_modified_date;//": "2024-08-01 04:29:06"

        public void CopyTo (Login data)
        {
            user_service_provider_id = data.user_service_provider_id;
            user_role_code = data.user_role_code;
            user_id = data.user_id;
            is_certified = data.is_certified;
            session_key = data.session_key;
            user_device_type_code = data.user_device_type_code;
            need_to_change_user_password = data.need_to_change_user_password;
            need_to_update_user_profile = data.need_to_update_user_profile;
            user_idx = data.user_idx;
            user_status_code = data.user_status_code;
            user_password_last_modified_date = data.user_password_last_modified_date;
        }

        public void send (string id, string pw)
        {
            var request = HTTPRequest.CreatePost(HTTPManager.uri_login.ToString(), HTTPManager.Instance.RequestFinishedCallback);
            //var request = new HTTPRequest(uri.ToString(), HTTPMethods.Post, RequestFinishedCallback);        

            request.UploadSettings.UploadStream = new MultipartFormDataStream()
                .AddField("id", id)
                .AddField("password", pw)
                .AddField("stayLoggedIn", "1");//세션유지용

            request.Send();

            Debug.Log($"send : {request.CurrentUri}");
        }        
        public void recv (string text)
        {
            CopyTo(HTTPManager.JsonToOject<Login>(text));            
        }
    }
}