using Mosframe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SchrollList : UIBehaviour, IDynamicScrollViewItem
{
    //フィールド
    private readonly Color[] colors = new Color[] {
            Color.cyan,
            Color.green,
    };

    public Text title;
    public Image background;

    private int dataIndex = -1;

    protected override void OnEnable()
    {

        base.OnEnable();
    }

    protected override void OnDisable()
    {

        base.OnDisable();
    }


    public void onUpdateItem(int index)
    {

        if (RealTimeInsertItemExample.I.data.Count > index)
        {

            this.dataIndex = index;
            this.updateItem();
        }
    }

    public void onClick()
    {

        if (this.dataIndex == -1) return;
        var data = RealTimeInsertItemExample.I.data[this.dataIndex];
        data.on = !data.on;

        this.updateItem();
    }

    private void updateItem()
    {

        if (this.dataIndex == -1) return;

        var data = RealTimeInsertItemExample.I.data[this.dataIndex];

        this.background.color = this.colors[Mathf.Abs(this.dataIndex) % this.colors.Length];

        if (data.on)
        {
            this.title.text = data.name + "(" + data.value + ")";
        }
        else
        {
            this.title.text = data.name + "(" + this.dataIndex.ToString("000") + ")";
        }

    }

}
