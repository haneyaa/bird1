using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public Sprite[] imgs;//小猪图片数组
    public GameObject scorePrefab;//分数预制体
    public GameObject boomPrefab;//爆炸特效预制体
    public AudioClip damageClip;//受伤音效
    public AudioClip dieClip;//死亡音效
    SpriteRenderer render;//图片渲染组件
    
    void Start()
    {
        render = GetComponent<SpriteRenderer>();//获取图片渲染组件
    }

    
    void Update()
    {
        
    }

    /// <summary>
    /// 有物理碰撞效果的函数
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.relativeVelocity.magnitude);
        //如果碰撞到的是小鸟
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("bird"))
        {
            Damage(collision.relativeVelocity.magnitude);//调用受伤方法，传入碰撞速度参数
        }
    }
    /// <summary>
    /// 小猪受伤
    /// 参数为碰撞速度
    /// </summary>
    void Damage(float speed)
    {
        Debug.Log("speed:"+ speed);
        if (speed > 5)
        {
            render.sprite = imgs[2];//把图片切换成死亡图片
            AudioClips.Instance.asource.PlayOneShot(dieClip);//播放小猪死亡音效
            GameObject score = Instantiate(scorePrefab);//实例化一个分数预制体
            score.transform.position = transform.position + new Vector3(0,1f,0);//分数生成在小猪头顶的位置

            Destroy(score, 1);//分数1秒之后消失

            GameObject boom = Instantiate(boomPrefab);//实例化一个爆炸特效
            boom.transform.position = transform.position + new Vector3(0,0.5f,0);//爆炸特效离小猪头顶0.5距离

            Destroy(boom,1);//爆炸特效1秒后消失

            Destroy(gameObject, 1);//小猪死亡以后，1秒消失
            BirdCtrl.Instance.pigs.Remove(this);
        }else if(speed > 1 && speed <= 5)
        {
            render.sprite = imgs[1];//小猪受伤
            AudioClips.Instance.asource.PlayOneShot(damageClip);//播放受伤音效
        }
    }
}
