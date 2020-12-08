# Gift Mobile
Приложение Gift Mobile для Android OS

## Описание проекта
Функционал приложения позволяет
* Просматривать, скачивать и отправлять на сервер привязанные к QR-коду видео
* Сканировать и распознавать QR-коды.

    1. Если QR-код не соответствует  требуемому формату, пользователю будет выведено сообщение об ошибке
    
![Alt Text](https://github.com/jeka1488/Library/blob/master/gif_1.gif)

    2. Если к QR-коду привязано видео, то его можно скачать во внутреннюю память телефона
    или перейти к просмотру во встроенном видео проигрывателе
    
![Alt Text](https://github.com/jeka1488/Library/blob/master/gif_2.gif)

    3. Если к QR-коду не привязано видео, то к этому QR коду можно привязать выбранное из галлерии или снятое на камеру видео
    
![Alt Text](https://github.com/jeka1488/Library/blob/master/gif_3.gif)


## Структура проекта
Проект разделен на модули (Clean Architecture).
* `data_layer` - модуль содержит классы для работы с данными 
* `domain-layer` - модуль содержит бизнес логику приложения (use-cases, сущности и интерфейсы репозиториев)
* `app` - модуль содержит презентационный слой приложения

## Архитектура проекта
* В данном проекте реализован паттерн проектирования *MVP Moxy*
* Сетевая инфраструктура построена на основе библиотеки *Retrofit*, построение асинхронных запросов осуществляется по средствам библиотек *RxJava* и *Volley*(для отправки видеофайла на сервер)
* В качестве QR-сканнера выступает *ML-Kit Barcode Scanner* совместно с *Jetpack* библиотекой *Camera X*
* Встроенный видео проигрыватель представляет собой реализацию библиотеки *ExoPlayer*
* Для минимизации количества кода в приложении используются библиотеки *Dagger 2* и *Butter Knife* 


## Системные требования
* Android OS с версией API 24+
