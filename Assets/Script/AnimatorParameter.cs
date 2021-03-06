using UnityEngine;

public abstract class AnimatorParameter { 
	public abstract class Enemy { 
		public static readonly int Death = Animator.StringToHash(nameof(Death));
		public static readonly int Attack = Animator.StringToHash(nameof(Attack));
		public static readonly int Scream = Animator.StringToHash(nameof(Scream));
	}

	public abstract class Evil_Wizard { 
		public static readonly int Death = Animator.StringToHash(nameof(Death));
		public static readonly int Life = Animator.StringToHash(nameof(Life));
		public static readonly int Attack = Animator.StringToHash(nameof(Attack));
		public static readonly int Speed = Animator.StringToHash(nameof(Speed));
		public static readonly int IsCasting = Animator.StringToHash(nameof(IsCasting));
		public static readonly int Evil_Laugh = Animator.StringToHash(nameof(Evil_Laugh));
	}

	public abstract class Hero { 
		public static readonly int Life = Animator.StringToHash(nameof(Life));
		public static readonly int Speed = Animator.StringToHash(nameof(Speed));
		public static readonly int Attack = Animator.StringToHash(nameof(Attack));
		public static readonly int IsJumping = Animator.StringToHash(nameof(IsJumping));
		public static readonly int Wave = Animator.StringToHash(nameof(Wave));
	}

	public abstract class NPC { 
		public static readonly int IsTalking = Animator.StringToHash(nameof(IsTalking));
		public static readonly int Surprised = Animator.StringToHash(nameof(Surprised));
	}

}
