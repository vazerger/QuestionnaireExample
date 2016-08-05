app.service("WebService", function ($http) {

    // создать user и взять его id
    this.getUserId = function (id) {
        return $http.get("/api/QApi/GetUserId/" + id);
    };

    // Ключи всех вопросов  
    this.getQuestionsKeys = function () {
        return $http.get("/api/QApi/GetKeyQuestions");
    };

    // Все вопросы  
    this.getQuestions = function () {
        return $http.get("/api/QApi/GetQuestions");
    };

    // Конкретный вопрос по его id
    this.getQuestion = function (id) {
        return $http.get("/api/QApi/GetQuestion/" + id);
    };

    //добавить ответ пользователя
    this.post = function (UserValue) {
        var request = $http({
            method: "post",
            url: "/api/QApi/PostQuestionUserValue",
            data: UserValue
        });
        return request;
    };

    // отчет по пользователю
    this.getValueUser = function (id) {
        return $http.get("/api/QApi/GetResultUser/" + id);
    };

    // общий отчет
    this.getGeneralReport = function () {
        return $http.get("/api/QApi/GetGeneralReport");
    };

    // второй общий отчет
    this.getMyReport = function () {
        return $http.get("/api/QApi/GetMyReport");
    };

    // сбросить результаты всех опросов
    this.delete = function () {
        var request = $http({
            method: "delete",
            url: "/api/QApi/DeleteQuestionsValue"
        });
        return request;
    };
});