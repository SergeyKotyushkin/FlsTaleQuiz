define([
        'knockout',
        'json!settings/quizOptions'
    ],
    function(ko, options) {
        'use strict';

        return function(params) {
            var self = this;

            var labels = options && options.labels || {};
            var settings = options && options.settings || {};

            self.loading = params && params.loading;
            self.showSubmit = params && params.showSubmit;
            self.addUserAnswer = params && params.addUserAnswer;

            self.currentQuestion = ko.observable();
            self.currentQuestionNumber = ko.observable(0);

            self.countOfQuestions = settings.countOfQuestions || 1;
            self.currentQuestionNumberLabelFormat = labels.currentQuestionNumberLabelFormat || '[Question #{0} of {1}]';

            self.currentQuestionNumberLabel = ko.pureComputed(_constructCurrentQuestionNumberLabel.bind(self));

            _loadQuestion.call(self);

            return {
                currentQuestionNumberLabel: self.currentQuestionNumberLabel,
                nextQuestionClick: _nextQuestionClick.bind(self),
                currentQuestion: self.currentQuestion,
                addUserAnswer: self.addUserAnswer
            };
        };

        function _constructCurrentQuestionNumberLabel() {
            var self = this;

            return self.currentQuestionNumberLabelFormat
                .replace('{0}', self.currentQuestionNumber())
                .replace('{1}', self.countOfQuestions);
        }

        function _nextQuestionClick() {
            var self = this;

            if (self.currentQuestionNumber() === self.countOfQuestions) {
                self.showSubmit();
                return;
            }

            _loadQuestion.call(self);
        }

        function _loadQuestion() {
            var self = this;

            self.loading(true);
            self.currentQuestion({
                id: '1',
                imageUrl:
                    'https://upload.wikimedia.org/wikipedia/commons/thumb/3/32/Auto_Racing_Green.svg/800px-Auto_Racing_Green.svg.png',
                text: 'some text',
                answers: [
                    { id: 1, text: 'Answer1' }, { id: 2, text: 'Answer2' }, { id: 3, text: 'Answer3' },
                    { id: 4, text: 'Answer4' }
                ]
            });
            _incrementCurrentQuestionNumber.call(self);
            self.loading(false);
        }

        function _incrementCurrentQuestionNumber() {
            var self = this;

            self.currentQuestionNumber(self.currentQuestionNumber() + 1);
        }
    }
);