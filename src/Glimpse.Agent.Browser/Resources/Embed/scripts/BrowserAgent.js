(function() {
    var common = (function() {
            return {
                getRequestId: function() {
                    // TODO: Get the current request id somehow
                    return $('#glimpse').attr('data-glimpse-id');
                }
            };
        })(),
        publishMessage = (function() {
            var getMessageId = function() {
                    // TODO: Generate random id for this message
                    return "e3599391-639a-4809-b7d3-10b286f59d3e";
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
        var connection = $.hubConnection("http://localhost:15999/glimpse/stream", { useDefaultPath: false });
        var messagePublisherHubProxy = connection.createHubProxy('remoteStreamMessagePublisherResource');

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