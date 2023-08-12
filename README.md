Stručné zadání:

Červeno-černý strom je datová struktura neboli soubor dat. Strom udržuje hodnoty seřazené. Červeno-černý strom je binární vyhledávací strom a rozdíl výšek dvou podstromů u všech vrcholů je nejvýš 1, neboli je to vyvážený strom. Červeno-černý strom má výšku logaritmickou vzhledem k počtu vrcholů, proto mají funkce logaritmické časy. Aby byl strom vyvážený, jeho vrcholy, které porušují vyváženost, potřebují rotovat. Červeno-černý strom má lepší výkon než AVL-strom, protože AVL-strom potřebuje rotovat, aby dodržoval vyváženosti, zatím Červeno-černý strom rotuje méně než AVL-strom. AVL-strom se dobře využívá v případě, kdy uživatel potřebuje hledat vrcholy a méně operací přidání a odebíraní vrcholů, zatím se Červeno-černý strom dobře využívá jak při hledání, tak i při přidání a odebíraní vrcholů.

Funkce červeno-černého stromu obsahuje: přidání a smazaní hodnoty, hledání minima, hledání maxima, hledání následníka, zjištění dané hodnoty ve stromě a zjištění počtu vrcholů ve stromě. 

Vstupní data jsou navzájem porovnatelná neboli úplně uspořádaná. Například: ve stromě můžou být čísla nebo řetězce, ale ne obojí současně a každá hodnota může být maximálně jednou.

Algoritmus funkce:

Pokud se chceme podívat, jak vypadá strom neboli se chceme podívat, či je dítě, tak spustíme funkce Print() pro reprezentaci stromu. 

Hlavní funkce stromu je insertFixTree() a removeFixTree(). Slouží k upravování stromu na správný tvar po přidání a smazání vrcholu. insertFixTree dělá něco, jen když jsou dva červené vrcholy nad sebou a removeFixTree řeší hlavně dvojitý černý vrchol. 

Pro funkce Insert (přidání vrcholu) najde nejdřív správné místo, potom přidává a upraví strom na správný tvar pomocí funkce insertFixTree. 

`  `Pro funkce remove (odstranění vrcholu) najde daný vrchol, potom smaže a upraví strom na správný tvar pomocí funkce removeFixTree.

Funkce hledání minima vrací rovnou další hodnotu iterovatelného objektu (první hodnota iterovatelného objektu je None, což znamená, že vrací první hodnotu v inorder stromu).

Funkce hledání maxima je potřeba obrátit celý iterator, potom vrací první hodnotu iteratoru.

`  `Funkce hledání následníka najde nejdřív daný vrchol, potom najdeme nejlevější vrchol jeho pravého dítěte.

Funkce Contains zjistí, zda daná hodnota je ve stromě. 








Reprezentace vstupních a výstupných dat ve funkce Main:

Vstup:

![](/vstup1.png) 

Výstup:

![](/vystup1.png)

Vstup:

![](/vstup2.png)

Výstup:

![](/vystup2.png)

Vstup:

![](/vstup3.png) 

Výstup:

![](/vystup3.png)

Vstup:

![](/vstup4.png)

Výstup:

![](/vystup4.png)

Zdroje:

<https://baike.baidu.com/item/%E7%BA%A2%E9%BB%91%E6%A0%91/2413209>

<https://zh.wikipedia.org/wiki/%E7%BA%A2%E9%BB%91%E6%A0%91>

<https://en.wikipedia.org/wiki/Red%E2%80%93black_tree>

Kniha: Průvodce labyrintem algoritmů – Martin Mareš, Tomáš Valla

Přednáška: Algoritmy a datové struktury – Jan Hric

Zápisky z přednášky:

<http://ktiml.mff.cuni.cz/~hric/vyuka/alg/ads1pr.pdf> 
