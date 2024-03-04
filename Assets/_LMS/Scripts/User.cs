using System;

[Serializable]
public class User
{
    public bool userLogin;
    public string userId;
    //public string userPassword;
    public string userScene;

    public User()
    {
        userLogin = Rest_api.playerLogin;
        userId = Rest_api.playerId;
        //userPassword = LoginSetting.playerPassword;
        userScene = Rest_api.playerScene;
    }
}