using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class Names
{
    public static List<string> ru_num = new List<string> { "Первая", "Вторая", "Третья" };
    public static List<string> ru_side = new List<string> { "Левая", "Центральная", "Правая" };
    public static List<string> ru_color = new List<string> { "Синяя", "Зелёная", "Красная", "Жёлтая", "Чёрная" };
    public static List<string> ru_form = new List<string> { "Круг", "Квадрат", "Треугольник", "Ромб"};

    public static List<string> en_num = new List<string> { "First", "Second", "Third" };
    public static List<string> en_side = new List<string> { "Left", "Center", "Right" };
    public static List<string> en_color = new List<string> { "Blue", "Green", "Red", "Yellow", "Black" };
    public static List<string> en_form = new List<string> { "Circle", "Square", "Triangle", "Rhombus" };

    public static List<string> tr_num = new List<string> { "Birinci", "Ikinci", "Üçüncü" };
    public static List<string> tr_side = new List<string> { "Sol", "Merkez", "Sağ" };
    public static List<string> tr_color = new List<string> { "Mavi", "Yeşil", "Kırmızı", "Sarı", "Siyah" };
    public static List<string> tr_form = new List<string> { "Daire", "Kare", "Üçgen", "Eşkenar Dörtgen" };

    public static List<string> NUM(string lang)
    {
        switch (lang)
        {
            case "en": return en_num;
            case "tr": return tr_num;
            default: return ru_num;
        }
    }

    public static List<string> SIDE(string lang)
    {
        switch (lang)
        {
            case "en": return en_side;
            case "tr": return tr_side;
            default: return ru_side;
        }
    }

    public static List<string> COLOR(string lang)
    {
        switch (lang)
        {
            case "en": return en_color;
            case "tr": return tr_color;
            default: return ru_color;
        }
    }

    public static List<string> FORM(string lang) 
    {
        switch (lang)
        {
            case "en": return en_form;
            case "tr": return tr_form;
            default: return ru_form;
        }
    }
}

public class SetState
{
    List<int> _numbers;
    List<NamedSprite> _forms;
    List<Color> _colors;

    // Индекс верной двери для проверки
    int _selectedIndex;
    // Пользовательский текст (цвет, форма, текст)
    UserText _userText;

    public SetState(Sprite _spriteCircle, Sprite _spriteRect, Sprite _spriteTree, Sprite _spriteRhombus)
    {
        UpdateState(_spriteCircle, _spriteRect, _spriteTree, _spriteRhombus);
    }

    public void UpdateState(Sprite _spriteCircle, Sprite _spriteRect, Sprite _spriteTree, Sprite _spriteRhombus)
    {
        string lang = Env.Instance.getLang();

        // Получаем перемешанные списки
        this._numbers = ListRandomInt();
        this._forms = ListRandomForm(lang, _spriteCircle, _spriteRect, _spriteTree, _spriteRhombus);
        this._colors = ListRandomColor();

        Color _UserTextColor = SelectRandomColor(_colors);
        Sprite _UserTextForm = SelectRandomForm(_forms).Sprite;
        string _selectedName;

        string _selectedType = SelectRandomType();
        switch (_selectedType)
        {
            case "number":
                int _selectedNum = SelectRandomInt(_numbers);
                this._selectedIndex = _numbers.IndexOf(_selectedNum);
                _selectedName = Names.NUM(lang)[_selectedNum-1];
                break;
            case "side":
                string _selectedSide = SelectRandomSide(lang);
                this._selectedIndex = Names.SIDE(lang).IndexOf(_selectedSide);
                _selectedName = _selectedSide;
                break;
            case "color":
                Color _selectedColor = SelectRandomColor(_colors);
                List<Color> s = new List<Color> { Color.blue, Color.green, Color.red, Color.yellow, Color.black };
                int i = s.IndexOf(_selectedColor);
                this._selectedIndex = _colors.IndexOf(_selectedColor);
                _selectedName = Names.COLOR(lang)[i];
                break;
            default:
                NamedSprite _selectedForm = SelectRandomForm(_forms);
                this._selectedIndex = _forms.IndexOf(_selectedForm);
                _selectedName = _selectedForm.Name;
                break;
        }

        this._userText = new UserText(_UserTextColor, _selectedName, _UserTextForm);
    }

    public bool CheckTrue(int variant)
    {
        return variant == _selectedIndex;
    }
    public UserText GetUserText()
    {
        return _userText;
    }
    public List<int> GetNumbers()
    {
        return _numbers;
    }
    public List<Sprite> GetForms()
    {
        List<Sprite> _sprites = new List<Sprite>(3);
        foreach (NamedSprite f in _forms)
        {
            _sprites.Add(f.Sprite);
        }
        return _sprites;
    }

    public List<Color> GetColors()
    {
        return _colors;
    }


    private List<Color> ListRandomColor()
    {
        List<Color> s = new List<Color> { Color.blue, Color.green, Color.red, Color.yellow, Color.black };

        System.Random rng = new System.Random();
        int n = s.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Color value = s[k];
            s[k] = s[n];
            s[n] = value;
        }
        return s.GetRange(0, 3);
    }

    private List<int> ListRandomInt()
    {
        List<int> ints = new List<int> { 1, 2, 3 };
        System.Random rng = new System.Random();
        int n = ints.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int value = ints[k];
            ints[k] = ints[n];
            ints[n] = value;
        }
        return ints;

    }

    private List<NamedSprite> ListRandomForm(string l, Sprite _spriteCircle, Sprite _spriteRect, Sprite _spriteTree, Sprite _spriteRhombus)
    {
        List<NamedSprite> s = new List<NamedSprite> { 
            new NamedSprite(Names.FORM(l)[0],_spriteCircle), 
            new NamedSprite(Names.FORM(l)[1], _spriteRect), 
            new NamedSprite(Names.FORM(l)[2], _spriteTree), 
            new NamedSprite(Names.FORM(l)[3], _spriteRhombus) 
        };

        System.Random rng = new System.Random();
        int n = s.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            NamedSprite value = s[k];
            s[k] = s[n];
            s[n] = value;
        }
        return s.GetRange(0, 3);
    }

    private string SelectRandomType()
    {
        List<string> types = new List<string> { "number", "side", "form", "color" };
        int idS = UnityEngine.Random.Range(0, 4);
        return types[idS];
    }

    private string SelectRandomSide(string l)
    {
        int idS = UnityEngine.Random.Range(0, 2);
        List<string> s = Names.SIDE(l).ToList();
        List<string> fs = s.Where((item, index) => index != _selectedIndex).ToList();
        return fs[idS];
    }

    private NamedSprite SelectRandomForm(List<NamedSprite> s)
    {
        int idS = UnityEngine.Random.Range(0, 2);
        List<NamedSprite> fs = s.Where((item, index) =>  index != _selectedIndex ).ToList();
        return fs[idS];
    }

    private int SelectRandomInt(List<int> s)
    {
        int idS = UnityEngine.Random.Range(0, 2);
        List<int> fs = s.Where((item, index) => index != _selectedIndex).ToList();
        return fs[idS];
    }

    private Color SelectRandomColor(List<Color> s)
    {
        int idS = UnityEngine.Random.Range(0, 2);
        List<Color> fs = s.Where((item, index) => index != _selectedIndex).ToList();
        return fs[idS];
    }
}


public class UserText
{
    public Color Color;
    public string Text;
    public Sprite Form;

    public UserText(Color _color, string _text, Sprite _form)
    {
        this.Color = _color;
        this.Text = _text;
        this.Form = _form;
    }
}


public class NamedSprite
{
    public string Name;
    public Sprite Sprite;

    public NamedSprite(string name, Sprite sprite)
    {
        this.Name = name;
        this.Sprite = sprite;
    }
}


public class DoorState : MonoBehaviour
{
    [SerializeField] Sprite _spriteCircle;
    [SerializeField] Sprite _spriteRect;
    [SerializeField] Sprite _spriteTree;
    [SerializeField] Sprite _spriteRhombus;

    SetState S;

    public void SetState()
    {
        this.S = new SetState(_spriteCircle, _spriteRect, _spriteTree, _spriteRhombus);
    }
    public void UpdateState()
    {
        this.S.UpdateState(_spriteCircle, _spriteRect, _spriteTree, _spriteRhombus);
    }

    public bool CheckTrue(int variant)
    {
        return this.S.CheckTrue(variant);
    }

    public UserText GetUserText()
    {
        return this.S.GetUserText();
    }


    public List<int> GetNumbers()
    {
        return this.S.GetNumbers();
    }

    public List<Sprite> GetForms()
    {
        return this.S.GetForms();
    }

    public List<Color> GetColors()
    {
        return this.S.GetColors();
    }
}
