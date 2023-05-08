using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LabraxStudio.UiAnimator
{
    public class BaseLinker <T> : MonoBehaviour
    {
       private T _element;

       public T GetLink() => _element;
       public void SetLink(T element) => _element = element;
    }
}
