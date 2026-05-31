# Quiz System
The Quiz system handles the questions and answers of the Quiz of the Department of Computer Science.
## QuizQuestions
Questions are stored in a .txt file (QuizQuestions.txt). Each question holds its question text, topic, subtopic, paragraph number, six answer choices, and the correct answer index.
## QuizContent
Each topic and therefore subtopic content are stored in a .txt file (QuizContent.txt). Each content holds a topic, subtopic, paragraph number, and the content text of this topic/subtopic.
## Quiz Manager
The Quiz Manager handles the core quiz loop. It picks a random question from the question pool, tracks whether a quiz is active, and processes the player's answer. A correct answer ends the quiz, while a wrong answer triggers a Punishment and ends the quiz.
The Quiz Manager also handles loading in the topic and subtopic folders, as well as the content .txt file with all the paragraphs and paragraphs numbers.
## Hint System
The Hint System is managed by the Quiz Manager. Hints are unlocked based on the previous wave completion percentage.
## Punishment System
The Punishment System is triggered by the Quiz Manager when the player gives a wrong answer.
## UI
The UI displays the Virus window, which represents the questions, the 6 answers, and the hints. It also displays the topic, and subtopic folder row, as well as the .txt files and the notepad. 