# BatchStealerBuilder (BSBuilder)
A builder for [BatchStealer](https://github.com/Takaovi/BatchStealer)

# How to use

* Install [Visual Studio](https://visualstudio.microsoft.com/vs/community/) (I used 2019 version) and build the program

# Screenshots

  <p align="center">
  <img src="https://i.imgur.com/mnyPkvw.png">
  </p>

# Todo
* Improve optimization
  * Delete unused functions
* Add/finish obfuscation function
* If the URL field is blank and user restarts the program, make it do something instead of not totally opening
* Possibly redo the whole code
  * It's stupid to make this batch file edit the file, when it could be actually building it. 
  * For each function there's variables or files what are then edited and placed to the (before the build blank) batch.
