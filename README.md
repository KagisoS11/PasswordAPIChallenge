# Password API Assessment

This project is a console application written in C# to solve a coding challenge involving a REST API for authentication and file submission.

## **Overview**
The application:
1. Generates a dictionary of password permutations for the word "password."
2. Attempts to authenticate a user (John) with the generated passwords using Basic Authentication against an API.
3. Submits a ZIP file containing:
   - The user's CV.
   - The dictionary file (`dict.txt`).
   - The source code of the program.

---

## **Features**
### 1. **Password Permutation Dictionary**
- Generates a file (`dict.txt`) containing all possible variations of the word "password" where:
  - Characters can be in upper or lower case.
  - Specific substitutions are applied:
    - `a` → `@`
    - `s` → `5`
    - `o` → `0`
- The file is used as input for the dictionary attack.

### 2. **Dictionary Attack**
- Uses the generated dictionary to make Basic Authentication API requests to:
- - Attempts each password until successful authentication.
- On success, retrieves a unique URL for file submission.

### 3. **File Submission**
- Compresses the following into a ZIP file:
- CV in PDF format.
- Dictionary file (`dict.txt`).
- Source code of the application.
- Encodes the ZIP file as a Base64 string.
- Submits the encoded file and user details (name, surname, email) as JSON via a POST request.

---

## **How It Works**

### 1. **Password Permutation Generation**
- The `GeneratePermutations` method uses recursion to create all combinations of the word "password" with character case changes and substitutions.
- The permutations are written to a file named `dict.txt`.

### 2. **Dictionary Attack**
- Reads the `dict.txt` file line by line.
- Makes HTTP GET requests to the authentication API using Basic Authentication with the username "John" and each password.
- Stops when the correct password is found, and the API returns the unique submission URL.

### 3. **File Submission**
- Prepares a ZIP file containing required files.
- Encodes the ZIP file into Base64 format.
- Sends a POST request to the URL obtained from the authentication step with the encoded file and user details.

---

## **Dependencies**
- **Newtonsoft.Json**: Used to handle JSON serialization and deserialization.
- No other external libraries are used.

---

## **How to Run**
1. Clone this repository:
 ```bash
 git clone https://github.com/YourUsername/PasswordApiAssessment.git
 cd PasswordApiAssessment
```
2. Open the project in Visual Studio Code.
   
3. Build and run the application.

