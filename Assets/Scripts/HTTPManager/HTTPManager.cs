using Best.HTTP;
using Best.HTTP.Request.Upload.Forms;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Best.HTTP.JSON;
using Best.HTTP.Cookies;
using UnityEngine.Assertions;
using System.IO;
using System;
using System.Net;

namespace X2R.HTTP
{
    public sealed class HTTPManager : MonoBehaviour
    {
        public enum API
        {
            none,
            login,
            getRecentlyEnrolledCourseId,
#if COURSE_LEARNING_CONTENTLIST
            getCourseLearningContentList,
#endif
            startLearning,
            updateLearningProgress,
        }

        public static readonly Uri uri_login = new Uri($"https://bukedu.vsquare.cc/api/user/login");
        public static readonly Uri uri_learning = new Uri($"https://bukedu.vsquare.cc/api/learning/");

        public bool UsingSessionCookie = false;

        [Header("[1] login")]
        public string id = "student01";
        public string pw = "1234qwer!";
        private Login login = new Login();

        public string courseCodeCode; // 과목 코드 (타일기능사 과목코드 : 20240001)
        public string courseContentCode; // 과목 콘텐츠 코드 (타일기능사 학습 콘텐츠(버전) : 2024000101)                       
        public RecentlyEnrolledCourseId recentlyEnrolledCourseId = new RecentlyEnrolledCourseId(); // 마지막으로 수강중 이었던 과목코드

        public bool IsRedayToLearning
        {
            get
            {
                if (recentlyEnrolledCourseId.recently_enrolled_course_id == -1)
                    return false;

                return true;
            }
        }

        [Header("[2] startLearning (학습 시작)")]
        public string courseLearningContentId;
        public string lessonItemId; // 과목 학습 콘텐츠 ID ...... 지정을 안하면 처음부터 시작한다 (중간부터 시작할때) 
        public string lessonSubitemId; // 학습 회차 ID        
        public StartLearning startLearning; // 학습 회차 페이지 ID        

        //#if UPDATE_LEARNING_PROGRESS
        [Header("[3] updateLearningProgress (학습 진도 업데이트)")]
        public string update_rate = "1"; // 비율, default 1
        private string update_bookmark; // 상태값 리스트
        public UpdateLearningProgress updateLearningProgress;
        //#endif

#if COURSE_LEARNING_CONTENTLIST
        [Header("[*4] getCourseLearningContentList (과목 학습 콘텐츠 목록 조회)")]
        public string includeCourseContentItemLesson = "1"; // 결과값에 콘텐츠 하위 lesson item 포함 여부
        private string _isAvailable = "1"; // 과목의 공개 여부를 설정
        public CourseLearningContentList courseLearningContentList;
#endif



        public string SessionKey
        {
            get
            {
                Assert.IsNotNull(login);
                return login.session_key;
            }
        }

        public delegate void OnRecivePacket(API api, bool succeed, string error_code, string message);
        private OnRecivePacket onReviePacket;

        public static string ObjectToJson(object obj) { return JsonUtility.ToJson(obj); }
        public static T JsonToOject<T>(string jsonData) { return JsonUtility.FromJson<T>(jsonData); }
        public void ShowCookies(Uri uri)
        {
            var cookies = CookieJar.Get(uri);

            foreach (var cookie in cookies)
            {
                Debug.Log($"Cookie info: {cookie}");
            }
        }

        public void SetCookieSessionKey(Uri uri)
        {
            if (UsingSessionCookie == true)
            {
                Assert.IsTrue(string.IsNullOrEmpty(HTTPManager.Instance.SessionKey) == false);
                CookieJar.Set(uri, new Best.HTTP.Cookies.Cookie("access_token", HTTPManager.Instance.SessionKey));
                Debug.Log($"SetCookieSessionKey ({HTTPManager.Instance.SessionKey})");
            }
        }

        #region singletone
        public static HTTPManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        #endregion


        private void Start()
        {
            CookieJar.Clear();
        }

        public void Init(string _id, string _pw, string _courseCodeCode, string _courseContentCode, OnRecivePacket callback)
        {
            Instance.id = _id;
            Instance.pw = _pw;
            Instance.courseCodeCode = _courseCodeCode;
            Instance.courseContentCode = _courseContentCode;

            onReviePacket = callback;
        }


        public bool OnLogin()
        {
            if (IsRedayToLearning == true)
            {
                Debug.LogWarning("이미 로그인 하였습니다.");
                return false;
            }

            login.send(id, pw);

            return true;
        }
        public bool OnStartLearning(int content_id, int item_id, int sub_id)
        {
            if (IsRedayToLearning == false)
            {
                Debug.LogError("로그인 정보를 확인하세요.");
                return false;
            }

            courseLearningContentId = content_id.ToString();
            lessonItemId = item_id.ToString();
            lessonSubitemId = sub_id.ToString();

            startLearning.send(recentlyEnrolledCourseId.recently_enrolled_course_id.ToString(),
                                content_id.ToString(),
                                item_id.ToString(),
                                sub_id.ToString());

            return true;
        }

        public bool OnUpdateLearning()
        {
            if (IsRedayToLearning == false)
            {
                Debug.LogError("로그인 정보를 확인하세요.");
                return false;
            }
            updateLearningProgress.send(courseLearningContentId,
            lessonItemId,
            lessonSubitemId,
            startLearning.GetTimeStamp,
            update_rate,
            update_bookmark);

            return true;
        }


        public void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Alpha1))
            //{
            //    login.send(id, pw);
            //}

            //if (Input.GetKeyDown(KeyCode.Alpha2))
            //{
            //    if (IsRedayToLearning)
            //    {
            //        startLearning.send(recentlyEnrolledCourseId.recently_enrolled_course_id.ToString(),
            //        courseLearningContentId,
            //        lessonItemId,
            //        lessonSubitemId);
            //    }
            //}

            ////#if UPDATE_LEARNING_PROGRESS
            //if (Input.GetKeyDown(KeyCode.Alpha3))
            //{
            //    if (IsRedayToLearning)
            //    {
            //        updateLearningProgress.send(courseLearningContentId,
            //        lessonItemId,
            //        lessonSubitemId,
            //        startLearning.GetTimeStamp,
            //        update_rate,
            //        update_bookmark);
            //    }
            //}
            //#endif


#if COURSE_LEARNING_CONTENTLIST
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                courseLearningContentList.send(recentlyEnrolledCourseId.recently_enrolled_course_id,
                    includeCourseContentItemLesson,
                    _isAvailable);
            }
#endif
        }

        public void send_getCourseLearningContentList()
        {
            StringBuilder post_url = new StringBuilder();
            post_url.Append(uri_learning.ToString());
            post_url.Append("getCourseLearningContentList");

            Debug.Log($"send_{post_url.ToString()}");

            var request = HTTPRequest.CreatePost(post_url.ToString(),
                                                 RequestFinishedCallback);


            request.UploadSettings.UploadStream = new MultipartFormDataStream()
                   .AddField("courseId", "4")
                   .AddField("includeCourseContentItemLesson", "1")
                   .AddField("isAvailable", "1");

            request.Send();
        }



        // 4. This callback is called when the request is finished. It might finished because of an error!
        public void RequestFinishedCallback(HTTPRequest req, HTTPResponse resp)
        {
            API api = API.none;

            string uri = resp.Request.CurrentUri.ToString();
            if (true == uri.Contains(API.login.ToString()))
            {
                api = API.login;
            }
            else if (true == uri.Contains(API.getRecentlyEnrolledCourseId.ToString()))
            {
                api = API.getRecentlyEnrolledCourseId;
            }
#if COURSE_LEARNING_CONTENTLIST
            else if (true == uri.Contains(API.getCourseLearningContentList.ToString()))
            {
                api = API.getCourseLearningContentList;
            }
#endif
            else if (true == uri.Contains(API.startLearning.ToString()))
            {
                api = API.startLearning;
            }
            else if (true == uri.Contains(API.updateLearningProgress.ToString()))
            {
                api = API.updateLearningProgress;
            }
            else
            {
                Debug.LogError("Not Define API");
                return;
            }

            switch (req.State)
            {
                case HTTPRequestStates.Finished:
                    if (resp.IsSuccess)
                    {
                        // 5. Here we can process the server's response
                        Debug.Log($"recv : {resp.DataAsText}");

                        Best.HTTP.JSON.LitJson.JsonData json_data = Best.HTTP.JSON.LitJson.JsonMapper.ToObject(resp.DataAsText);
                        Assert.IsNotNull(json_data);

                        Best.HTTP.JSON.LitJson.JsonData json_data_code = json_data["code"];
                        Best.HTTP.JSON.LitJson.JsonData json_data_message = json_data["message"];

                        bool succeed = (true == string.Equals(json_data_code.ToJson(), "10000"));

                        if (succeed == false)
                        {
                            Debug.LogWarning($"에러코드 ({json_data_code} : {json_data_message})");
                        }
                        else
                        {
                            Best.HTTP.JSON.LitJson.JsonData json_data_body = json_data["body"];
                            Assert.IsNotNull(json_data_body);

                            string json_body = json_data_body.ToJson();

                            if (api == API.login)
                            {
#if UNITY_EDITOR
                                ShowCookies(req.CurrentUri);
#endif
                                login.recv(json_body);

                                recentlyEnrolledCourseId.send(courseCodeCode, courseContentCode);
                            }
                            else if (api == API.getRecentlyEnrolledCourseId)
                            {
                                recentlyEnrolledCourseId.recive(json_body);
                            }
#if COURSE_LEARNING_CONTENTLIST
                            else if (api == API.getCourseLearningContentList)
                            {
                                courseLearningContentList.recive(json_body);
                            }
#endif
                            else if (api == API.startLearning)
                            {
                                startLearning.recive(json_body);
                            }
                            else if (api == API.updateLearningProgress)
                            {
                                updateLearningProgress.recive(json_body);
                            }
                            else
                            {
                                Debug.LogError("Not Define PacketType");
                            }
                        }

                        if (onReviePacket != null)
                        {
                            onReviePacket(api, succeed, json_data_code.ToJson(), json_data_message.ToJson());
                        }
                    }
                    else
                    {
                        // 6. Error handling
                        Debug.Log($"Server sent an error: {resp.StatusCode}-{resp.Message}");
                        if (onReviePacket != null)
                        {
                            onReviePacket(api, false, resp.StatusCode.ToString(), resp.Message);
                        }

                    }
                    break;

                default:
                    // 6. Error handling
                    Debug.LogError($"Request finished with error! Request state: {req.State}");
                    if (onReviePacket != null)
                    {
                        onReviePacket(api, false, resp.StatusCode.ToString(), resp.Message);
                    }
                    break;
            }
        }
    }
}