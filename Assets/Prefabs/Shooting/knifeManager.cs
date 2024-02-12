using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeManager : MonoBehaviour
{


    public void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            IknifeInteraction interaction = other.GetComponent<IknifeInteraction>();
            if(interaction != null)
            {
                interaction.knifeInteract();
            }

            Destroy(gameObject);
        }
    }

}
