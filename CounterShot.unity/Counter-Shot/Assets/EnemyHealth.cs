using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;
    // Start is called before the first frame update


    [Header("LayoutGroup")]
    public LayoutGroup targetLayoutGroup;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void AddDamage(int damage)
    {
        currentHealth = currentHealth - damage;
    }
    //out of time but what i am planning on doing is getting the number of images in the layout group, then destroying/disabling them according to the damage numbers




    /*
    using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GetLayoutGroupImages : MonoBehaviour
{
    public LayoutGroup targetLayoutGroup; // Assign your Layout Group in the Inspector

    void Start()
    {
        if (targetLayoutGroup == null)
        {
            Debug.LogError("Target Layout Group is not assigned!");
            return;
        }

        List<Image> imagesInLayoutGroup = GetImagesInLayoutGroup(targetLayoutGroup);

        Debug.Log($"Found {imagesInLayoutGroup.Count} images in the layout group.");

        foreach (Image image in imagesInLayoutGroup)
        {
            Debug.Log($"Image found: {image.gameObject.name}");
        }
    }

    public List<Image> GetImagesInLayoutGroup(LayoutGroup layoutGroup)
    {
        List<Image> images = new List<Image>();

        // Iterate through all children of the GameObject containing the Layout Group
        // The children are the elements managed by the Layout Group
        foreach (Transform childTransform in layoutGroup.transform)
        {
            Image image = childTransform.GetComponent<Image>();
            if (image != null)
            {
                images.Add(image);
            }
        }
        return images;
    }
}*/
}
