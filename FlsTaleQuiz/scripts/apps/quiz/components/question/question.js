define([
        'knockout',
        'json!settings/quizOptions'
    ],
    function(ko, options) {
        'use strict';

        return function(params) {
            var self = this;

            var labels = options && options.labels || {};

            var model = params && params.question || {};

            self.answerId = model.answerId;
            self.text = model.text;
            self.imageUrl = model.imageUrl;
            self.answers = model.answers || [];
            self.selectedAnswerId = ko.observable();

            self.nextQuestionHandler = params && params.nextQuestionHandler;
            self.addUserAnswer = params && params.addUserAnswer;

            var answerButtonLabel = labels.answerButtonLabel || '[To answer]';
            var answerButtonClick = _answerButtonClick.bind(self);

            return {
                text: self.text,
                imageUrl: self.imageUrl,
                answers: self.answers,
                selectedAnswerId: self.selectedAnswerId,
                answerButtonLabel: answerButtonLabel,
                answerButtonClick: answerButtonClick
            };
        };

        function _answerButtonClick() {
            var self = this;

            var selectedAnswerId = self.selectedAnswerId();
            if (!selectedAnswerId) {
                return;
            }

            self.addUserAnswer(self.answerId, selectedAnswerId);
            self.nextQuestionHandler();
        }
    }
);