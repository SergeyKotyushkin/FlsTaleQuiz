(function quizConfigModule() {
    'use strict';

    require.config({
        baseUrl: './',
        paths: {
            'knockout': 'scripts/vendor/knockout-3.4.2',
            'jquery': 'scripts/vendor/jquery-3.3.1.min',
            'knockout.validation': 'scripts/vendor/plugins/knockout.validation',
            'text': 'scripts/vendor/plugins/text',
            'json': 'scripts/vendor/plugins/json'
        }
    });

    require([
            'scripts/apps/quiz/quiz.app',
            'scripts/apps/quiz/setup/component-factory',
            'scripts/apps/quiz/setup/component-registrator'
        ],
        function _(quizApp, componentFactory, componentRegistrator) {

            _initComponents(componentFactory, componentRegistrator);

            var domElement = 'quiz-app';
            var settings = {};

            quizApp.initialize(domElement, settings);
        }
    );

    function _initComponents(componentFactory, componentRegistrator) {
        var componentNames = ['intro', 'test', 'question', 'submit', 'finish'];
        var components = componentNames.map(function _createComponent(componentName) {
            return componentFactory.create(componentName);
        });

        components.map(function _registerComponent(component) {
            componentRegistrator.register(component);
        });
    }
})();
