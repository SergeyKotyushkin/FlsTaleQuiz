define([
        'knockout',
        'jquery',
        'json!settings/quizOptions',
        'knockout.validation'
    ],
    function(ko, $, options) {
        'use strict';

        return function(params) {
            var self = this;

            var labels = options && options.labels || {};

            _initValidation.call(self, labels);
            
            self.loading = params && params.loading;
            self.userAnswers = params && params.userAnswers || [];
            self.showFinish = params && params.showFinish;

            self.isReadyForSubmit = ko.pureComputed(_isReadyForSubmit.bind(self));

            var firstNameLabel = labels.firstNameLabel || '[First Name]';
            var lastNameLabel = labels.lastNameLabel || '[Last Name]';
            var emailLabel = labels.emailLabel || '[Email]';

            var submitButtonLabel = labels.submitButtonLabel || '[Submit]';
            var submitButtonClick = _submit.bind(self);

            return {
                firstName: self.firstName,
                lastName: self.lastName,
                email: self.email,
                firstNameLabel: firstNameLabel,
                lastNameLabel: lastNameLabel,
                emailLabel: emailLabel,
                submitButtonLabel: submitButtonLabel,
                submitButtonClick: submitButtonClick,
                isReadyForSubmit: self.isReadyForSubmit
            };
        };

        function _initValidation(labels) {
            var self = this;

            var firstNameRequiredMessage = labels.firstNameRequiredMessage || '[First Name is required]';
            var lastNameRequiredMessage = labels.lastNameRequiredMessage || '[Last Name is required]';
            var emailRequiredMessage = labels.emailRequiredMessage || '[Email is required]';
            var emailIncorrectMessage = labels.emailIncorrectMessage || '[Email is incorrect]';

            ko.validation.init({ insertMessages: false }, true);
            
            self.firstName = ko.observable().extend({ required: { message: firstNameRequiredMessage } });
            self.lastName = ko.observable().extend({ required: { message: lastNameRequiredMessage } });
            self.email = ko.observable().extend({
                required: { message: emailRequiredMessage },
                email: { message: emailIncorrectMessage }
            });

            self.errors = ko.validation.group(self);
        }

        function _isReadyForSubmit() {
            var self = this;

            return self.userAnswers &&
                ko.unwrap(self.userAnswers()).length &&
                self.isValid();
        }

        function _submit() {
            var self = this;

            if (self.errors().length) {
                self.errors.showAllMessages(true);
                return;
            }

            _saveResults.call(self);
        }

        function _saveResults() {
            var self = this;

            self.loading(true);
            $.post('result/saveResults',
                    {
                        firstName: self.firstName(),
                        lastName: self.lastName(),
                        email: self.email(),
                        userAnswers: self.userAnswers()
                    },
                    function _onSuccess(result) {
                        console.log(JSON.stringify(result));
                        self.showFinish();
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
    }
);