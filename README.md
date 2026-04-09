FileMonitoringApi

REST API сервис для мониторинга CSV файла Путь к отслеживаемому файлу задаётся в конфиге Построен на ASP.NET Core Web API (.NET 8)

Эндпоинты
GET /filemonitor/count

Возвращает количество записей в отслеживаемом CSV файле Заголовок файла (первая строка) не считается записью Если файл не существует — возвращает 0

GET /filemonitor/export

Возвращает отслеживаемый CSV файл пользователю для скачивания После экспорта файл переименовывается по шаблону и перемещается в папку out/

Шаблон переименования: [yyyy.MM.dd_HH.mm][старое_имя_файла] Пример: [2026.04.08_14.30][data.csv]

Папка out/ создаётся автоматически в том же каталоге что и отслеживаемый файл

Технологии

ASP.NET Core Web API (.NET 8)
Без внешних зависимостей — используются встроенные API файловой системы .NET

Конфигурация

В файле appsettings.json укажи путь к отслеживаемому CSV файлу:

{
  "FileMonitor": {
    "FilePath": "C:\\TestFiles\\data.csv"
  }
}

Запуск

Требования:

.NET 8 SDK
Шаги:

Создаём CSV файл по пути указанному в appsettings.json
Обновим FilePath в appsettings.json если путь отличается
Запустим проект: dotnet run
Swagger UI откроется по адресу https://localhost:{port}/swagger

Примеры запросов и ответов

Получить количество записей:

GET /filemonitor/count
{
  "count": 3
}
Если файл отсутствует:

GET /filemonitor/count
{
  "count": 0
}
Экспортировать файл:

GET /filemonitor/export
Скачивание файла: [2026.04.08_14.30]_[data.csv]
Файл не найден — 404 Not Found:

{
  "error": "Monitored file not found."
}

Структура проекта

FileMonitoringApi/ Controllers/ FileMonitorController.cs Services/ FileMonitorService.cs appsettings.json Program.cs
