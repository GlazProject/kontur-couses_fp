## Задание FileSenderRailway

1. Сделать класс Document неизменяемым
2. Отделить чистую функцию PrepareFileToSend от Side-эффектов
3. Добавить дополнительную информацию в тексты ошибок:
    * Версию формата, если он не поддерживается
    * Дату создания, если документ слишком старый
4. Переделать логику на исключениях на Result<T>.
   Можно и нужно менять интерфейсы зависимостей!
5. Переделать тесты на PrepareFileToSend. Предварительно
   PrepareFileToSend должен стать декларативным.
