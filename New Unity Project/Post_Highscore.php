<?php
	$scoreFile = './highscore.txt';
	$currentHighScore = (int) file_get_contents($scoreFile);
	$inputScore = (int) $_POST["scoreKey"];
	if($inputScore > $currentHighScore){
		file_put+contents($scoreFile, $inputScore);
		echo "New high score updated successfully\n High score is: " . $inputScore;
	}
	else{
		echo $inputScore . " is not a new high score\nHighscore is: " . $currentHighScore;
	}
?>