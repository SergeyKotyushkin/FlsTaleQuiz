﻿define([
        'knockout',
        'jquery',
        'json!settings/quizOptions'
    ],
    function(ko, $, options) {
        'use strict';

        return function(params) {
            var self = this;

            var settings = options && options.settings || {};

            self.loading = params && params.loading;
            self.userAnswers = params && params.userAnswers;
            self.showSubmit = params && params.showSubmit;
            self.addUserAnswer = params && params.addUserAnswer;

            self.currentQuestion = ko.observable();
            self.currentQuestionNumber = ko.observable(0);

            self.countOfQuestions = settings.countOfQuestions || 1;
            
            _loadQuestion.call(self);

            return {
                nextQuestionClick: _nextQuestionClick.bind(self),
                currentQuestion: self.currentQuestion,
                addUserAnswer: self.addUserAnswer,
                currentQuestionNumber: self.currentQuestionNumber,
                countOfQuestions: self.countOfQuestions
            };
        };

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
                    console.log('error');
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