using Best.HTTP;
using Best.HTTP.Request.Upload.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace X2R.HTTP
{
    [System.Serializable]
    public class RecentlyEnrolledCourseId
    {
        public int recently_enrolled_course_id = -1;
        public void CopyTo(RecentlyEnrolledCourseId data)
        {
            recently_enrolled_course_id = data.recently_enrolled_course_id;
        }

        public void send(string _courseCodeCode, string _courseContentCode)
        {
            StringBuilder post_url = new StringBuilder();
            post_url.Append(HTTPManager.uri_learning.ToString());
            post_url.Append(HTTPManager.API.getRecentlyEnrolledCourseId.ToString ());

            HTTPManager.Instance.SetCookieSessionKey(new Uri(post_url.ToString()));

            var request = HTTPRequest.CreatePost(post_url.ToString(), HTTPManager.Instance.RequestFinishedCallback);

            request.UploadSettings.UploadStream = new MultipartFormDataStream()
                   .AddField("courseCodeCode", _courseCodeCode)
                   .AddField("courseContentCode", _courseContentCode);
                   //.AddField("statusCodeList", "1"); // 필수 입력은 아님

            request.Send();

            Debug.Log($"send : {request.CurrentUri}");
        }

        public void recive(string text)
        {   
            CopyTo(HTTPManager.JsonToOject<RecentlyEnrolledCourseId>(text));
        }
    }
}
