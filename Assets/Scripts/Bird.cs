using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public GameObject boomPrefab;//爆炸特效

    bool isMove = false;//小鸟是否可以拖动
    public Vector3 startPos;//小鸟的初始位置
    Vector3 targePos;//拖动的目标位置
    Rigidbody2D rig;//2d刚体组件
    SpringJoint2D joint;//弹簧关节组件
    LineRenderer leftRender;//左画线组件
    LineRenderer rightRender;//右画线组件
    Transform leftPos;//左画线位置
    Transform rightPos;//右画线位置
    TestMyTrail trail;//拖尾组件脚本

    bool isGround = false;//小鸟没有着地

    private void Awake()
    {
        Init();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        Move();
    }

    /// <summary>
    /// 初始化方法
    /// </summary>
    void Init()
    {
        startPos = transform.position;//记录小鸟的初始位置
        rig = GetComponent<Rigidbody2D>();//先查找小鸟身上绑定的刚体
        joint = GetComponent<SpringJoint2D>();//查找小鸟身上的弹簧关节
        leftRender = GameObject.Find("left").GetComponent<LineRenderer>();//查找左画线组件
        rightRender = GameObject.Find("right").GetComponent<LineRenderer>();//查找右画线组件
        leftPos = transform.parent.Find("left/leftPos");//查找左画线位置
        rightPos = transform.parent.Find("right/rightPos");//查找右画线位置
        trail = GetComponent<TestMyTrail>();//查找拖尾脚本组件
    }

    /// <summary>
    /// 鼠标按下，小鸟可以拖动
    /// </summary>
    private void OnMouseDown()
    {
        isMove = true;
        
    }

    /// <summary>
    /// 鼠标抬起，小鸟停止拖动
    /// </summary>
    private void OnMouseUp()
    {
        isMove = false;
        joint.enabled = false;//抬起鼠标，弹簧失效
        leftRender.enabled = false;//启动左画线组件
        rightRender.enabled = false;//启用右画线组件
        AudioClip clip = AudioClips.Instance.audios[1];//小鸟飞出去的声音
        AudioClips.Instance.asource.clip = clip;//把声音片段指定给播放组件
        AudioClips.Instance.asource.Play();//播放音效
        trail.StartTrails();//调用拖尾方法
        Invoke("Die",2);//扔出去1秒以后死亡
    }

    /// <summary>
    /// 小鸟拖动
    /// </summary>
    private void Move()
    {
        if (isMove)//如果小鸟可以移动
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);//让小鸟的位置与鼠标相同，实现拖动
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);//z轴调整
            targePos = transform.position;//目标位置
            Vector3 dir = (targePos - startPos).normalized;//目标方向
            float dis = Vector3.Distance(targePos, startPos);//求出目标位置和初始位置的距离
            if(dis > 1.5f)
            {
                transform.position = startPos + dir * 1.5f;//距离不能超过1.5
            }

            Line();
        }
    }

    /// <summary>
    /// 画线方法
    /// </summary>
    void Line()
    {
        leftRender.enabled = true;//启动左画线组件
        rightRender.enabled = true;//启用右画线组件
        leftRender.SetPosition(0,leftPos.position);//线段从弹弓开始
        leftRender.SetPosition(1,transform.position);//线段在小鸟结束，画出从弹弓到小鸟的线段
        //画出从右边弹弓开始，到小鸟结束的线段
        rightRender.SetPosition(0,rightPos.position);
        rightRender.SetPosition(1,transform.position);
    }

    /// <summary>
    /// 小鸟死亡
    /// </summary>
    public void Die()
    {
        GameObject boom = Instantiate(boomPrefab);//实例化一个爆炸特效
        boom.transform.position = transform.position + new Vector3(0,0.5f,0);//特效位置在小鸟上边一段距离，为了不挡住小鸟
        Destroy(boom, 0.5f);//0.5秒以后删除爆炸特效
        AudioClips.Instance.asource.clip = AudioClips.Instance.audios[0];//指定音乐片段为小鸟死亡音效
        AudioClips.Instance.asource.PlayOneShot(AudioClips.Instance.audios[0]);//播放音乐片段
        Destroy(gameObject,0.5f);//小鸟0.5秒后消失
        BirdCtrl.Instance.birdGos.Remove(gameObject);
        BirdCtrl.Instance.GameOver();

    }

    /// <summary>
    /// 碰撞方法
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("ground"))//如果碰到的是地面
        {
            if (!isGround)//没着地
            {
               //Invoke("Die",0.5f);//小鸟0.5秒后直接死亡
                isGround = true;
            }
            
        }
    }

    /// <summary>
    /// 设置小鸟为当前可控制的小鸟
    /// </summary>
    public void SetCurBird(bool flag)
    {
        joint.enabled = flag;
    }
}
