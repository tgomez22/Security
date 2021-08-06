<?

$encodedSecret = "3d3d516343746d4d6d6c315669563362";

function decodeSecret($secret) {
    return base64_decode(strrev(hex2bin($secret)));
}

decodeSecret("3d3d516343746d4d6d6c315669563362")
?>