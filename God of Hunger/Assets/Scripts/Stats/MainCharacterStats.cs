using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class MainCharacterStats : CharacterStats
{

    public override void Die()
    {
        base.Die();
        
        // TEMP reload scene on death
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);

        // Death effect

        // Game Over
    }
    
}
