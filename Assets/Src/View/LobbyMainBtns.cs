using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMainBtns : BaseView {

	public void UserMatchClick()
    {
        Logger.Log("LobbyMainBtns.UserMatchClick");
        LobbyMainBtnsModel currentModel = _baseModel as LobbyMainBtnsModel;
        if(currentModel.userMatchClickCall != null)
        {
            currentModel.userMatchClickCall();
        }
    }

    public void FriendMatchClick()
    {
        Logger.Log("LobbyMainBtns.FriendMatchClick");
        LobbyMainBtnsModel currentModel = _baseModel as LobbyMainBtnsModel;
        if (currentModel.friendMatchClickCall != null)
        {
            currentModel.friendMatchClickCall();
        }
    }
}
