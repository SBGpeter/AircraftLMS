using Best.HTTP;
using Best.HTTP.Request.Upload.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace X2R.HTTP
{
#if UPDATE_LEARNING_PROGRESS
    [System.Serializable]
    public class UpdateLearningProgress
    {
        public string _temp;
        public void CopyTo(UpdateLearningProgress data)
        {
        }

        public void send(string _courseLearningContentId, string _lessonItemId, string _lessonSubitemId, string _timestamp, string _rate, string _bookmark)
        {
            StringBuilder post_url = new StringBuilder();
            post_url.Append(HTTPManager.uri_learning.ToString());
            post_url.Append(HTTPManager.API.updateLearningProgress.ToString ());

            HTTPManager.Instance.SetCookieSessionKey(new Uri(post_url.ToString()));

            var request = HTTPRequest.CreatePost(post_url.ToString(), HTTPManager.Instance.RequestFinishedCallback);

            request.UploadSettings.UploadStream = new MultipartFormDataStream()
                   .AddField("courseLearningContentId", _courseLearningContentId)
                   .AddField("lessonItemId", _lessonItemId)
                   .AddField("lessonSubitemId", _lessonSubitemId)
                   .AddField("timestamp", _timestamp)
                   .AddField("rate", _rate)
                   //.AddField("bookmark", _bookmark)
                ;

            request.Send();

            Debug.Log($"send : {request.CurrentUri}");
            Debug.Log($"courseid:{_courseLearningContentId}, lessonItemId:{_lessonItemId}, lessonSubitemId:{_lessonSubitemId}, timestamp:{_timestamp}");
        }

        public void recive(string text)
        {
            _temp = text;
            Debug.Log(text);
            //CopyTo(HTTPManager.JsonToOject<UpdateLearningProgress>(text));
        }
    }
#endif
}