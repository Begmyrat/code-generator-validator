# Code Generator & Validator
This console application generates and validates codes without saving to any storage. 
## Table of Contents
- [Features](#features)
- [Installation & Usage](#installation--usage)
- [How It Works](#how-it-works)

## Features
Generates and validates codes without saving to any storage.

## Installation & Usage
1. Clone the repository: `git clone [https://github.com/sayitkilic/code-generator-validator.git]`
2. Navigate to the project directory: `cd code-generator-validator`
3. Run

## How It Works

* The character set to be used in code generation has been determined.
* To ensure that the code continues from its current state when generating code in different places, to avoid generating repetitive code, and to have a unique verification method, a singleton instance has been created.
* During the initialization of this instance, the following parameters are initialized:
  * The number of digits in the code
  * The number of different values each digit can take and the possible values for each digit individually
  * The regex pattern used for validation
  * A counter which indicates how many codes generated (Also used for code generation)
* During this process, separate lists are created for the possible values that each digit can take.
* Each list is filled separately by randomly selecting characters from the character set, based on the number of different values that a digit can take.
* Randomly selecting characters for each digit reduces the likelihood of predicting future codes as it introduces an element of randomness and makes the patterns less predictable.
* The randomly selected character is removed from the character set. This ensures that all elements of the character set are used as much as possible.
* When the character set is completely depleted, it is re-initialized.
* After determining the characters that each digit can take individually, a regex pattern is created that matches the specific criteria for each digit. The validation of the codes is performed based on this regex pattern.
* When generating code:
  * Based on the value of the counter, the decision is made regarding which character from the lists will be used. For example each digit can take 2 values
    * Counter = 0 => indices = [0][0][0]
    * Counter = 1 => indices = [1][0][0] 
    * Counter = 2 => indices = [0][1][0] 
    * Counter = 3 => indices = [0][0][1] 
