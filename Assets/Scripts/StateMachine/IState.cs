using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    void OnEnter(Enemy enemy);//chuyển trạng thái gọi 1 lần duy nhất
    void OnExcute(Enemy enemy);//liên tục update 
    void OnExit(Enemy enemy);//thoát khỏi trạng thái
}
