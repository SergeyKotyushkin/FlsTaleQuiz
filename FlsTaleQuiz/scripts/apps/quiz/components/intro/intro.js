define([
        'knockout',
        'json!settings/quizOptions'
    ],
    function(ko, options) {
        'use strict';

        return function(params) {
            var labels = options && options.labels || {};

            var showTest = params && params.showTest;

            return {
                startTestButtonLabel: labels.startTestButtonLabel || '[Start test button]',
                startTestButtonClick: _startTestButtonClick.bind(null, showTest)
            };
        };

        function _startTestButtonClick(showTestCallback) {
            showTestCallback && showTestCallback();
        }
    }
);