using Best.HTTP;
using Best.HTTP.Request.Upload.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace X2R.HTTP
{
    [System.Serializable]
    public class LatestStudentAttendanceLog
    {
        public string course_id;
        public string student_user_idx;
        public string lesson_subitem_required_learning_time_in_seconds;
        public string lesson_item_title;
        public string lesson_subitem_id;
        public string last_modified_date;
        public string lesson_id;
        public string lesson_subitem_title;
        public string course_learning_content_id;
        public string lesson_title;
        public string total_learning_time_in_seconds;
        public string registered_date;
        public string lesson_item_id;
        public string timestamp;
    }

    public class SubitemList
    {
        public string code;
        public string mobile_compatibility_code;
        public List<Subitem> subitem_list;
        public string display_order;
        public string last_modified_date;
        public string id;
        public string registered_date;
        public string title;
        public string lesson_id;
    }
    [System.Serializable]
    public class Subitem
    {
        public string code;
        public string display_order;
        public string id;
        public string title;
        public string required_learning_time_in_seconds;
        public string lesson_item_id;
    }
    [System.Serializable]
    public class StudentAttendanceItem
    {
        public string lesson_subitem_title;
        public string course_learning_content_id;
        public string status_code;
        public string status_updated_date;
        public string student_user_idx;
        public string lesson_subitem_required_learning_time_in_seconds;
        public string lesson_item_title;
        public string lesson_subitem_id;
        public string lesson_title;
        public string total_learning_time_in_seconds;
        public string lesson_id;
        public string lesson_item_id;
    }

    [System.Serializable]
    public class StudentAttendanceLog
    {
        public string course_id;
        public string lesson_item_student_attendance_status_code;
        public string student_user_idx;
        public string lesson_subitem_id;
        public string last_modified_date;
        public string lesson_id;
        public string course_learning_content_id;
        public string lesson_student_attendance_status_code;
        public string total_learning_time_in_seconds;
        public string registered_date;
        public string lesson_subitem_student_attendance_status_code;
        public string lesson_item_id;
        public string timestamp;
    }

    [System.Serializable]
    public class StudentAttendance
    {
        public string status_code;
        public string student_user_idx;
        public string student_user_id;
        public string registered_from_country;
        public string last_modified_date;
        public string is_manually_modified;
        public string last_modified_by_user_idx;
        public string last_modified_from_device_type_code;
        public string course_learning_content_id;
        public string status_updated_date;
        public List<StudentAttendanceItem> item_list;
        public string registered_from_ip_address;
        public string course_learning_content_type_code;
        public string lesson_title;
        public string registered_by_user_idx;
        public string last_modified_from_ip_address;
        public string registered_from_device_type_code;
        public string total_learning_time_in_seconds;
        public string registered_date;
        public string course_learning_content_title;
        public string learning_progress;
        public string last_modified_from_country;
        public string course_learning_content_attendance_count;
    }

    [System.Serializable]
    public class StartLearning
    {
        public string is_deleted;
        public string code;
        public LatestStudentAttendanceLog latest_student_attendance_log;
        public List<SubitemList> item_list;
        public string last_modified_date;
        public StudentAttendanceLog current_student_attendance_log;
        public string id;
        public string registered_date;
        public string title;
        public StudentAttendance student_attendance;
        public string is_available;        

        public void CopyTo(StartLearning clone)
        {
            is_deleted = clone.is_deleted;
            code = clone.code;
            latest_student_attendance_log = clone.latest_student_attendance_log;
            item_list = clone.item_list;
            last_modified_date = clone.last_modified_date;
            current_student_attendance_log = clone.current_student_attendance_log;
            id = clone.id;
            registered_date = clone.registered_date;
            title = clone.title;
            student_attendance = clone.student_attendance;
            is_available = clone.is_available; ;
    }

        public void send(string _courseId, string _courseLearningContentId, string _lessonItemId, string _lessonSubitemId)
        {
            StringBuilder post_url = new StringBuilder();
            post_url.Append(HTTPManager.uri_learning.ToString());
            post_url.Append(HTTPManager.API.startLearning);

            HTTPManager.Instance.SetCookieSessionKey(new Uri(post_url.ToString()));

            var request = HTTPRequest.CreatePost(post_url.ToString(), HTTPManager.Instance.RequestFinishedCallback);

            request.UploadSettings.UploadStream = new MultipartFormDataStream()
                   .AddField("courseId", _courseId)
                   .AddField("courseLearningContentId", _courseLearningContentId)
                   .AddField("lessonItemId", _lessonItemId)
                   .AddField("lessonSubitemId", _lessonSubitemId)
                   ;
            request.Send();

            Debug.Log($"send : {request.CurrentUri}");
            Debug.Log($"courseid:{_courseId}, courseLearningContentId:{_courseLearningContentId}, lessonItemId:{_lessonItemId}, lessonSubitemId:{_lessonSubitemId}");
        }

        public void recive(string text)
        {            
            var data = HTTPManager.JsonToOject<StartLearning>(text);
            CopyTo(data);            

            string timeStamp = GetTimeStamp;
            if (true == string.IsNullOrEmpty(timeStamp))
            {
                Debug.LogError("Faild Recv GetTimeStamp");
            }
            else
            {
                Debug.Log($"timestamp : {GetTimeStamp}");
            }
        }

        public string GetTimeStamp
        {
            get
            {
                return current_student_attendance_log.timestamp;
            }
        }
    }
}