define([
        'knockout',
        'jquery',
        'json!settings/quizOptions'
    ],
    function(ko, $, options) {
        'use strict';

        return function(params) {
            var self = this;

            var labels = options && options.labels || {};
            var settings = options && options.settings || {};

            self.loading = params && params.loading;
            self.userAnswers = params && params.userAnswers;
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
            $.post('question/getRandom',
                    {
                        excludedQuestionsIds: self.userAnswers().map(function
                            _mapQuestionId(userAnswer) {
                                return userAnswer.questionId;
                            })
                    },
                    function _onSuccess(result) {
                        if (!result || !result.question) {
                            return;
                        }

                        self.currentQuestion(result.question);
                        _incrementCurrentQuestionNumber.call(self);
                    },
                    'json'
                )
                .fail(function _onError() {
                    console.log("error");
                })
                .always(function _always() {
                    self.loading(false);
                });
        }

        function _incrementCurrentQuestionNumber() {
            var self = this;

            self.currentQuestionNumber(self.currentQuestionNumber() + 1);
        }
    }
);