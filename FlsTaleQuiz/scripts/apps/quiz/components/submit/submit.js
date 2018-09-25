define([
        'knockout',
        'jquery',
        'knockout.validation'
    ],
    function(ko, $) {
        'use strict';

        return function(params) {
            var self = this;

            self.email = ko.observable();
            self.name = ko.observable();
            self.phone = ko.observable();
            self.comment = ko.observable();

            _initValidation.call(self);
            
            self.loading = params && params.loading;
            self.userAnswersIds = params && params.userAnswersIds || [];
            self.showFinish = params && params.showFinish;
            self.showModalErrorMessage = params && params.showModalErrorMessage;

            self.isReadyForSubmit = ko.pureComputed(_isReadyForSubmit.bind(self));

            var submitButtonClick = _submit.bind(self);

            return {
                email: self.email,
                name: self.name,
                phone: self.phone,
                comment: self.comment,
                submitButtonClick: submitButtonClick,
                isReadyForSubmit: self.isReadyForSubmit
            };
        };

        function _initValidation() {
            var self = this;
            
            ko.validation.init({ insertMessages: false }, true);
            
            self.email.extend({
                required: { message: 'Email is required' },
                email: { message: 'Email is incorrect' }
            });

            self.errors = ko.validation.group(self);
        }

        function _isReadyForSubmit() {
            var self = this;

            return self.userAnswersIds &&
                ko.unwrap(self.userAnswersIds()).length &&
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
                        email: self.email(),
                        name: self.name(),
                        phone: self.phone(),
                        comment: self.comment(),
                        userAnswersIds: self.userAnswersIds()
                    },
                    function _onSuccess(response) {
                        if (response.hasErrors) {
                            if (response.usedEmail) {
                                self.showModalErrorMessage('Entered email has already been used. Try another one.');
                                return;
                            }

                            if (response.MailSendError) {
                                self.showModalErrorMessage('Oops, something went wrong. Email has not been sent.');
                                return;
                            }

                            if (!response.mailSent) {
                                self.showModalErrorMessage(response.MailSendError
                                    ? 'Oops, something went wrong. Email has not been sent.'
                                    : 'Oops, something went wrong. Sorry for inconvenience.');
                                return;
                            }

                            // ? Mail was sent but results were not saved in db
                            //if (!response.mailSent) {
                            //    self.showModalErrorMessage('Oops, something went wrong. Sorry for inconvenience.');
                            //    return;
                            //}
                        }

                        self.showFinish();
                    },
                    'json'
                )
                .fail(function _onError() {
                    self.showModalErrorMessage('Oops, something went wrong. Sorry for inconvenience.');
                })
                .always(function _always() {
                    self.loading(false);
                });
        }
    }
);