# CSV Upload

New accounts can be created en masses using a CSV upload. Only new accounts found in the file are added to the system. Existing accounts are ignored.

## File Format

| Column Index | Column Name | Example | Notes |
|:------------:|:-----------:|:-------:|:-----:|
| 0 | Student Email | `jjohnson07@laurel.k12.pa.us` | Must be a laurel.k12.pa.us email or the record is ignored. |
| 1 | Student Graduation Year | `2007` | |
| 2 | Starting Account Balance | `150.00` | |
| 3 | Parent Emails (Comma or Semicolon Delimited) | `parent1@gmail.com;parent2@gmail.com` | Optional. These must be Google account emails as they need to match the email used by the parent to sign in. |