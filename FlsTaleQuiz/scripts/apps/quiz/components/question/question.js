define([
        'knockout'
    ],
    function(ko) {
        'use strict';

        return function(params) {
            var self = this;
            
            var model = params && params.question || {};

            self.id = model.id;
            self.text = model.text;
            self.imageUrl = model.imageUrl;
            self.answers = model.answers || [];
            self.selectedAnswerId = ko.observable();

            self.nextQuestionHandler = params && params.nextQuestionHandler;
            self.addUserAnswerId = params && params.addUserAnswerId;

            var answerButtonClick = _answerButtonClick.bind(self);

            return {
                text: self.text,
                imageUrl: self.imageUrl,
                answers: self.answers,
                selectedAnswerId: self.selectedAnswerId,
                answerButtonClick: answerButtonClick
            };
        };

        function _answerButtonClick() {
            var self = this;

            var selectedAnswerId = self.selectedAnswerId();
            if (!selectedAnswerId) {
                return;
            }

            self.addUserAnswerId(selectedAnswerId);
            self.nextQuestionHandler();
        }
    }
);