﻿
# Native Android Elements For Unity + Unity 3D example

A [Unity 3D][unity] classes that helps unity developers using some native android elements (Toats,Alert,Calendar,TextIntent)

[unity]: https://unity3d.com

## Getting started

clone this repo 

Now, just:

1. Open the project in Unity.
2. Switch to Android platform in Build Settings.
3. Build and Run The exemple scene!


Tested in unity 2019.2 and android 9

## How to use 
 import the folder NativeAndroidElements in your unity project





## Toast

    using NativeAndroidElements;
    ...
     Toast.Show("YOUR_MESSAGE", Toast.LENGTH_LONG|Toast.LENGTH_SHORT);

    


## Alert

        using NativeAndroidElements;
        ...
        Alert alertDiaolog = new Alert();  
        //adding callbacks for success and fail buttons
        // you can charge the delegate directly with needed fucntions 
        // alertDiaolog.onFail += () => { Toast.Show("adding function to delegate", Toast.LENGTH_LONG); };
        
	    alertDiaolog.onFailLitener(() => { Toast.Show("calling a functon from on fail", Toast.LENGTH_LONG); });
	    alertDiaolog.onSucessLitener(() => { Toast.Show("on sucess from event listener", Toast.LENGTH_LONG); });  
    //success and fail buttons are optional
	    alertDiaolog.Show("YOUR_TITLE", "YOUR_MESSAGE", "success button label","fail button label");




## Calendar

 

       using NativeAndroidElements;
        ...
        Calendar cal = new Calendar();  
        //callback for when date selected
    	cal.onSelectListener(() => { Toast.Show(cal.getSelectedDate().ToString(), Toast.LENGTH_LONG); });  
    	cal.Show();


## TextIntent

 

  

         using NativeAndroidElements;
            ... 
           TextIntent intent = new TextIntent();  
		   intent.ShareText("SHARE_SUBJECT", "SHARE_MESSAGE", "SHARE_TAG");


## Contact & feedback

Let me know your thoughts and feedback [dhiaa.kahri@gmail.com](dhiaa.kahri@gmail.com)
F eelfree to contribute and use this package as u want :)
this package was made by dhia kahri 

Thanks for [Suneet Agrawal] for his articles on medium about using native android in untiy
g
