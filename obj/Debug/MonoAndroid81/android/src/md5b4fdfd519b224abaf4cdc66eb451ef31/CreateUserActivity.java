package md5b4fdfd519b224abaf4cdc66eb451ef31;


public class CreateUserActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_FinishActivity:(Landroid/view/View;)V:__export__\n" +
			"n_CreateUser:(Landroid/view/View;)V:__export__\n" +
			"";
		mono.android.Runtime.register ("PracticalCodingTest.CreateUserActivity, PracticalCodingTest", CreateUserActivity.class, __md_methods);
	}


	public CreateUserActivity ()
	{
		super ();
		if (getClass () == CreateUserActivity.class)
			mono.android.TypeManager.Activate ("PracticalCodingTest.CreateUserActivity, PracticalCodingTest", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void FinishActivity (android.view.View p0)
	{
		n_FinishActivity (p0);
	}

	private native void n_FinishActivity (android.view.View p0);


	public void CreateUser (android.view.View p0)
	{
		n_CreateUser (p0);
	}

	private native void n_CreateUser (android.view.View p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
