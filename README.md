# HyperSeriesTest

Features : 

- You can run the application on Android (with the .apk)

- Video Features : 
    - You can play and pause a video on the client page
    - You can change the time of the video with the slider 
    - The time of the video is displayed 
    - You can go into fullscreen by rotating your phone or by clicking on a button

- You can see the serie name, and the episode name and the number of views by clicking on it

- You can switch between the editor and the client page by scrolling or by clicking on the "editeur" or the "client" button. 

- In the editor page, you can change the episode name and the number of views. You can then save it by clicking on the "save" button. If you return to the client page, you can see the modifications. If you restart the app, what you saved before will also be loaded.

A diagram is available at the root of the repository.
Original Diagram Link : https://drive.google.com/file/d/1BhTE3VuI-dGk37at6Yf87D0zWNVE99dF/view?usp=sharing


Time spent for each exercise : 
- Exercie 1 : UI Integration : **5h**
	- Problems : 
		- Keeping aspect ratio (of the video / image) while putting an UI element right under the video

- Exercise 2 : Vertical Layout : **4h30**

- Exercise 3 : Horizontal Layout : **1h30**
    - Problems :
        - What layout should I do ? 
            - Solution : For most video apps, it just goes into fullscreen mode

- Exercise 4 : Video : **4h**
	- Problems : 
		- When changing the time, the video doesn't load the new frames immediatly so the bar goes back to the previous value. Tried to use sendFrameReadyEvents and frameReady, but sometimes frames are already loaded, so it doesn't work (no fixes found)
		- When going to fullscreen, the transition isn't perfect
		
- Exercise 5 : Documentation : **1h30**