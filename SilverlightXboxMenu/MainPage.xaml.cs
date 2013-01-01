using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace SilverlightXboxMenu
{
	public partial class MainPage
	{
	    public MainPage()
	    {
	        InitializeComponent();
	        Application.Current.Host.Content.Resized += Content_Resized;
	    }

	    void Content_Resized(object sender, EventArgs e)
	    {
	        var screenWidth = Application.Current.Host.Content.ActualWidth;  
            var applicationWidth = screenWidth * 5;

            // large enough for all elements to be drawn
	        this.Width = applicationWidth;
	        this.LayoutRoot.Width = applicationWidth;
            this.MenuContainer.Width = applicationWidth;
            
            // visible user screen 
            this.FullScreenColumn.Width = new GridLength(screenWidth);
            this.MenuHeaderGrid.Width = screenWidth;

            // adjust profile to sit in the top right hand corner
            this.UserProfileContainer.Margin = new Thickness(screenWidth - 150, 10, 0, 0);
            
	        // Update the animations so the menus can scroll in and be visible when clicked
            // (notice we are skipping the first bing menu animation as this is already visible)
            var vsStates = ((VisualStateGroup)VisualStateManager.GetVisualStateGroups(this.LayoutRoot)[0]).States;
            for (var i = 1; i < vsStates.Count; i++)
	        {
                // set to negative so the menu scrolls into view from offscreen the right hand side
                ((DoubleAnimation)((VisualState)vsStates[i]).Storyboard.Children[0]).To = screenWidth * (i * -1);
	        }
            
            //refresh current VisualState as the To's sizes have changed...
            var currentState = ((VisualStateGroup)VisualStateManager.GetVisualStateGroups(this.LayoutRoot)[0]).CurrentState;
	        if (currentState != null)
	        {
	            currentState.Storyboard.Stop();
	            currentState.Storyboard.Begin();
	        }

            //update the background image to be fully visible on window resize
            this.ImageBackground.Width = screenWidth;                             
            this.ImageBackground.Height = Application.Current.Host.Content.ActualHeight;
        }
	}
}