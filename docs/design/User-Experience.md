# User Experience

## Sign-In Experience

Upon visiting https://my.spartan.band, users will be asked to sign in (there are no unauthenticated pages). After signing in, users are redirected to https://my.spartan.band/accounts. What happens next depends on the user's role.

### Teachers

Teachers are presented with a list of all accounts in the system. These users have the ability to add new accounts as well as assign parents to those accounts. To create new accounts, teachers can create them one by one in the UI or upload a CSV file. Uploading a CSV file ensures only new accounts found in the file are added.

### Parents

Parents are presented with a list of all accounts they have been added to. If they only have access to one, they are automatically redirected to that account (https://my.spartan.band/accounts/{accountId}).

### Students

The logic for students is technically the same for parents, but we would expect they will always be redirected as a student generally shouldn't have more than one account. This isn't a gurantee, so we should code for the possibility. But it is unlikely.

If a student signs in before an account is created for them, they are presented with a notice to contact Mr. Croach or Mr. Johnson. Perhaps an email can be sent to alert staff as well.

## Accounts and Account IDs

Account IDs by default are just the email address of the student that owns the account. The same can be said for user IDs of students. They are just their email address.

## New Accounts and 