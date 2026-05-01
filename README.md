# Advanced_Web_Project
## Project Proposal

### Application Info
Musichord aims to be a companion to a user’s streaming service (specifically Spotify for the scope of this project). Users will be able to create an account and connect their Spotify accounts to it. From there, they can connect with other friends on the service and engage in a variety of social features. These social features include:
- Profile customization (listing top songs or albums).
- Rating and reviewing albums found on Spotify.
- A social feed to see what a user’s friends are listening to.
- Song and album “leaderboards”, ranking users who have listened to a song or album the most.
- Various ways of displaying a user’s Spotify statistics.
This application uses Spotify’s API to read data about the user’s account. This data would include their recent and current listening, as well as historical data about their account, like their affinity with certain songs or artists. Although most data would be coming from Spotify, some data will need to be locally stored for historical or social purposes, including friendships or a user’s previous listening data.
### Technologies
- Spotify’s Developer REST API
- Razor/MVC
- SQLite Database for Local Storage
### Functionalities as User Stories
- As a user, I want to be able to add friends into my Musichord network.
- As a user, I want to be able to see what music I have been listening to recently in an easy to perceive way.
- As a user, I want to be able to see what my friends are currently listening to.
- As a user, I want to be able to favorite and rate certain artists or albums.
- As a user, I want an easy onboarding process to create a Musichord account and link it to Spotify.
- As a user, I want fun and interesting ways to see my listening statistics and history.
- As an administrator, I want to be able to compile stats about what Musichord members are listening to for community statistics and better interaction with users.

## AI Disclosure
AI Chats and Agents were used throughout the length of the development process of this project. It was used as an assistive tool to help with bug fixes and help to explain implementation steps whenever the team was stuck. An example of this was using the GitHub Copilot Agent to insert comments where code would need to be implemented in order to add a specific feature. This allowed members to develop the product themselves and use AI as a tool for reference more than for code. AI was also used for bug fixes where the team members could not figure out a solution themselves after several non-AI attempts at it.


## Accessibility Principles Followed
We followed the accessibility principles outlined for the project, ultimately focusing on these principles 
- Text Alternatives for Non-text Content
  - All images on the site are labeled with alt tags to describe what is being displayed.  Forms are labeled with aria labels to help screen readers process through forms.
- Text is readable and understandable
  - Color schemes were chosen with good contrast to ensure readability of the site, either by using white text on a dark background
