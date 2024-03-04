using Best.HTTP;
using Best.HTTP.Request.Upload.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace X2R.HTTP
{
#if COURSE_LEARNING_CONTENTLIST
    [System.Serializable]
    public class CourseLearningContentList
    {
        public string recive_text;

        public void send(string _courseId, string _includeCourseContentItemLesson, string _isAvailable)
        {
            StringBuilder post_url = new StringBuilder();
            post_url.Append(HTTPManager.uri_learning.ToString());
            post_url.Append(HTTPManager.API.getCourseLearningContentList.ToString ());

            HTTPManager.Instance.SetCookieSessionKey(new Uri(post_url.ToString()));

            var request = HTTPRequest.CreatePost(post_url.ToString(), HTTPManager.Instance.RequestFinishedCallback);

            request.UploadSettings.UploadStream = new MultipartFormDataStream()
                   .AddField("courseId", _courseId)
                   .AddField("includeCourseContentItemLesson", _includeCourseContentItemLesson)
                   .AddField("isAvailable", _isAvailable);

            request.Send();

            Debug.Log($"send : {request.CurrentUri}");
        }

        public void recive(string text)
        {
            recive_text = text;
            Debug.Log(text); 
        }
    }
#endif
}