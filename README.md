<!DOCTYPE html>
<html lang="ru">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<body>
    <h2>Грамматика:</h2>
    <p>1) DEF -> letter LISTNAME</p>
    <p>2) LISTNAME -> letter LISTNAME | = ASSIGNTMENT</p>
    <p>3) ASSIGNTMENT -> [ ITEMS</p>
    <p>4) ITEMS -> [+|-] NUMBER | " STRING</p>
    <p>5) NUMBER -> digit NUMBERREM</p>
    <p>6) NUMBERREM -> , ITEMS | ] | digit NUMBERREM | . DECIMAL</p>
    <p>7) DECIMAL -> digit DECIMALREM</p>
    <p>8) DECIMALREM -> , ITEMS | ] | digit DECIMALREM</p>
    <p>9) STRING -> "] | ", ITEMS | symbol STRING</p>
    <h2>Граф конечного автомата:</h2>
    <img src="https://github.com/bruhspirit/coursework/assets/160126744/4c4a64db-c3ff-465e-89b7-8218055bf55b">
    <h2>Примеры:</h2>
    <img src="https://github.com/bruhspirit/coursework/assets/160126744/c657d1d6-c27e-4fe3-bbb6-819aec60729b">
    <img src="https://github.com/bruhspirit/coursework/assets/160126744/1c473336-a7c7-407c-b010-237524b50d26">
    <img src="https://github.com/bruhspirit/coursework/assets/160126744/4042aef5-c632-452f-89f7-1c3a6609a008">
    <img src="https://github.com/bruhspirit/coursework/assets/160126744/5a8e18a6-008a-4a54-b6dc-cff7cbfd3fd7">
     <img src="https://github.com/bruhspirit/coursework/assets/160126744/1c8d341b-bce5-44bd-bf1a-20ecffe81c32">
    <h2>Лексический анализатор (сканер):</h2>
    <img src="https://github.com/bruhspirit/coursework/assets/160126744/c24d0b59-ca46-4141-8299-217bc3ec960c" alt=""> 
    <h2>Цель:</h2>
    <p>Изучить назначение лексического анализатора. Спроектировать алгоритм и выполнить программную реализацию сканера.</p>
    <p>В соответсвии с вариантом задания необходимо:</p>
    <p>1) Спроектировать диаграмму состояний сканера</p>
    <p>2) Разработать лексический анализатор, позволяющий выделить в тексте лексемы, иные символы считать недопустимыми</p>
    <p>3) Встроить сканер в ранее разработанный интерфейс текстового редактора. Учесть, что текст для разбора может состоять из множества строк.</p>
    <h2>Вариант задания: Объявление списка с инициализацией на языке Python</h2>
    <h2>List = [1 , 2 , 3 , "gfd" , 2,3]</h2>  
    <img src="https://github.com/bruhspirit/coursework/assets/160126744/bf5ad544-9c67-4e9b-8c7f-e716ae6926b9" alt=""> 
    <h2>Текстовые примеры</h2>  
    <img src="https://github.com/bruhspirit/coursework/assets/160126744/7de6a84c-03fc-4086-8f8d-5b0236a27039" alt=""> 
    <img src="https://github.com/bruhspirit/coursework/assets/160126744/db14f733-339b-489e-945e-6dfb71961fbc" alt=""> 
    <img src="https://github.com/bruhspirit/coursework/assets/160126744/94f54fde-032f-47d1-a847-c2799949abf1" alt=""> 
</html>

