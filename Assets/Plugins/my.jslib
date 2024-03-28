mergeInto(LibraryManager.library, {  
  thisIsDesktop: function()
  {
    return ysdk.deviceInfo.isDesktop()
  },
  
  RequestData: function() {
    player.getStats().then(d => {
      // window.alert(JSON.stringify(d));
      myGameInstance.SendMessage('API', 'GetData', JSON.stringify(d));
    });
  },

  ShowAD: function() {
    ysdk.adv.showFullscreenAdv({
                    callbacks: {
                        onClose: getCallback('onClose'),
                        onOpen: getCallback('onOpen'),
                        onError: getCallback('onError'),
                        onOffline: getCallback('onOffline')
                    }
                });
  },

  SaveData: function(str) {
    // window.alert(UTF8ToString(str));
    var myobj = JSON.parse(UTF8ToString(str));
    ysdk.isAvailableMethod('leaderboards.setLeaderboardScore').then(able => {
      if (able) {
        lb.setLeaderboardScore('scoreboard', myobj.Score);
      }
    });
    player.setStats(myobj, true);
  },

  RequestUser: function() {
    let usr = {
      userId: player.getUniqueID(),
      userName: player.getName(),
      userIconStr: player.getPhoto("medium"),
    };
    myGameInstance.SendMessage('API', 'GetUser', JSON.stringify(usr));
  },

  RequestLang: function() {
    var lang = ysdk.environment.i18n.lang;
    var bufferSize = lengthBytesUTF8(lang) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(lang, buffer, bufferSize);
    return buffer;
  },
});