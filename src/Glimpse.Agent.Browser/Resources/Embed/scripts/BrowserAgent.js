(function() {
    var common = (function() {
            return {
                getRequestId: function() {
                    // TODO: Get the current request id somehow
                    return $('#glimpse').attr('data-glimpse-id');
                },
                getGuid: function() {
                    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
                            var r = Math.random()*16|0, v = c == 'x' ? r : (r&0x3|0x8);
                            return v.toString(16);
                        });
                }
            };
        })(),
        publishMessage = (function() {
            var getMessageId = function() { 
                    return common.getGuid();
                },
                getCurrentTime = function() {
                    return new Date();
                },
                buildContext = function() {
                    return {
                	    id: common.getRequestId(),
                        type: 'Request'
                    };
                },
                buildMessage = function(type, payload) {
                    return {
                        type: type,
                        payload: payload,
                        context: buildContext()
                    }
                },
                preProcessMessage = function(message) {
                    if (!message.id) {
                        message.id = getMessageId();
                    }

                    if (!message.time) {
                        message.time = getCurrentTime();
                    }

                    // TODO: Probably shouldn't be here. Just not
                    //       sure if it should be in every message
                    message.uri = document.URL;
                };

            return function(type, payload, proxy) {
                preProcessMessage(payload);

                // TODO: Need to make sure what ever we do here works cross browser - looking at you JSON
                proxy.invoke('handleMessage', buildMessage(type, JSON.stringify(payload)));
            };
        })();

    var processRUM = function(proxy) {
            var timingsRaw = (window.performance || window.mozPerformance || window.msPerformance || window.webkitPerformance || {}).timing;

            publishMessage('browser.rum', timingsRaw, proxy);
        },
        insertUI = function() {
            $(function() {
                $(document.body).append('<a target="_blank" href="http://localhost:1561/#' + common.getRequestId() + '">Launch Glimpse</a>');
            });
        };

    (function() {
        var connection = $.hubConnection("http://localhost:15999/Glimpse/Data/Stream", { useDefaultPath: false });
        var messagePublisherHubProxy = connection.createHubProxy('webSocketChannelReceiver');

        connection.start({ withCredentials: false })
            .done(function() {
                console.log('Now connected, connection ID=' + connection.id);

                processRUM(messagePublisherHubProxy);

                insertUI();
            })
            .fail(function() {
                console.log('Could not connect');
            });
    })();
})();