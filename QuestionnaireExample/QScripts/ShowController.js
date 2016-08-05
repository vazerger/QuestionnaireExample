app.controller('ShowController', function ($scope, $location, WebService, ShareData) {

    // загружаем вопросы
    QuestionsRecord();

    // подтверждение введения имени
    $scope.addName = function () {

        if ($scope.name) {

            // имя введено
            $scope.uName = true;
            $scope.error = '';
            
            // создать пользователя и записать в глобальную переменную id
            UserCreateGetId($scope.name);
            
            // если есть вопросы, то берем первый
            if ($scope.Questions.length > 0) {
                $scope.qcount = null;
                NextQuestion();
            }
            else {
                $scope.error = 'Вопросы отсутствуют!';
            }
        }
        else {
            $scope.error = 'Введите Имя!';
        }
    };

    // следующий вопрос
    $scope.qNext = function () {
        $scope.error = null;
        var questions = $scope.Questions[$scope.qcount - 1];
        var question = $scope.Question.data;

        // проверка на введенное значение
        if ($scope.count != null) {
            if ($scope.count > 0 && $scope.count <= question.n) {

                // заносим в базу значение
                var value = {
                    questionid: questions.id,
                    userid: ShareData.userkey,
                    value: $scope.count
                };

                console.log(value);

                var valuePost = WebService.post(value);

                valuePost.then(function (pl) {
                    //alert("Сохранено.");
                },
                      function (errorPl) {
                          $scope.error = 'не сохранено', errorPl;
                      });

                $scope.count = null; // обнуляет вводимый ответ
                NextQuestion();
            }
            else {
                $scope.error = 'Введите число от 1 до ' + question.n;
            }
        }
        else {
            $scope.error = 'Введите число';
            }

    };

    function NextQuestion()
    {
        $.go = function (path) {
            $location.path(path);
        };

        var AllkeysQuestions = $scope.Questions;
      
        if ($scope.qcount >= AllkeysQuestions.length) {
            // на выход
            $scope.qcount = null;
            $.go('/result');          
        }
        else {

            // номер вопроса
            if ($scope.qcount == null) {
                $scope.qcount = 1
            }
            else {
                $scope.qcount++;
            }

            // здесь идет обращение за следующим вопросом по его ID
            var idQuestion = AllkeysQuestions[$scope.qcount - 1].id;
            
            var GetQuestion = WebService.getQuestion(idQuestion);
            GetQuestion.then(function (pl) {
                $scope.Question = pl
            },
            function (errorPl) {
                $scope.error = errorPl;
            });
            
        }
    }

    // выбрать ключи всех вопросов
    // можно выбрать сразу все вопросы в один массив, но это более накладно по ресурсам
    // лучше взять все id и обращаться за очередным по мере необходимости через сервис
    function QuestionsRecord() {

        var SetKeys = [];
        var GetQuestions = WebService.getQuestionsKeys();
        
        GetQuestions.then(function (pl) {
            // глобально, но можно посчитать количество и обратиться по необходимости
            $scope.Questions = pl.data;
        },           
        function (errorPl) {
            $scope.error = errorPl;
        });
    }

    function UserCreateGetId(name) {

        var GetUserkey = WebService.getUserId(name);        
        GetUserkey.then(function (pl) {
            // глобально, но можно посчитать количество и обратиться по необходимости
            ShareData.userkey = pl.data;
        },           
        function (errorPl) {
            $scope.error = errorPl;
        });
    }

});