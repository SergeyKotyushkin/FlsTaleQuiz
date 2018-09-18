define([
        'json!settings/quizOptions'
    ],
    function(options) {
        'use strict';

        return function() {
            var labels = options && options.labels || {};

            var finishText = labels.finishText || '[Finish]';

            return {
                finishText: finishText
            };
        };
    }
);