// stellartools.js

/**
 * Decrypts the encrypted secret key using the provided password.
 * @param {string} encryptedData - The encrypted secret key in Base64 format.
 * @param {string} password - The password used for decryption.
 * @returns {Promise<string>} - The decrypted secret key.
 */
async function decryptSecret(encryptedData, password) {
    const combinedBuffer = base64ToArrayBuffer(encryptedData);
    const combined = new Uint8Array(combinedBuffer);

    // Extract salt, iv, and encrypted content
    const salt = combined.slice(0, 16);
    const iv = combined.slice(16, 28);
    const data = combined.slice(28);

    const keyMaterial = await window.crypto.subtle.importKey(
        "raw",
        new TextEncoder().encode(password),
        { name: "PBKDF2" },
        false,
        ["deriveKey"]
    );

    const key = await window.crypto.subtle.deriveKey(
        {
            name: "PBKDF2",
            salt: salt,
            iterations: 100000,
            hash: "SHA-256",
        },
        keyMaterial,
        { name: "AES-GCM", length: 256 },
        true,
        ["decrypt"]
    );

    const decrypted = await window.crypto.subtle.decrypt(
        {
            name: "AES-GCM",
            iv: iv,
        },
        key,
        data
    );

    return new TextDecoder().decode(decrypted);
}

/**
 * Encrypts the secret key using the provided password.
 * @param {string} secretKey - The secret key to encrypt.
 * @param {string} password - The password used for encryption.
 * @returns {Promise<string>} - The encrypted secret key in Base64 format.
 */
async function getEncryptedSecret(secretKey, password) {
    const encoder = new TextEncoder();
    const data = encoder.encode(secretKey);
    const salt = crypto.getRandomValues(new Uint8Array(16));

    const keyMaterial = await window.crypto.subtle.importKey(
        "raw",
        encoder.encode(password),
        { name: "PBKDF2" },
        false,
        ["deriveKey"]
    );

    const key = await window.crypto.subtle.deriveKey(
        {
            name: "PBKDF2",
            salt: salt,
            iterations: 100000,
            hash: "SHA-256",
        },
        keyMaterial,
        { name: "AES-GCM", length: 256 },
        true,
        ["encrypt"]
    );

    const iv = crypto.getRandomValues(new Uint8Array(12));

    const encrypted = await window.crypto.subtle.encrypt(
        {
            name: "AES-GCM",
            iv: iv,
        },
        key,
        data
    );

    // Combine salt, iv, and encrypted data
    const combined = new Uint8Array(salt.byteLength + iv.byteLength + encrypted.byteLength);
    combined.set(salt, 0);
    combined.set(iv, salt.byteLength);
    combined.set(new Uint8Array(encrypted), salt.byteLength + iv.byteLength);

    // Convert to Base64 for storage/transmission
    return arrayBufferToBase64(combined.buffer);
}

/**
 * Converts an ArrayBuffer to a Base64 string.
 * @param {ArrayBuffer} buffer - The buffer to convert.
 * @returns {string} - The Base64 encoded string.
 */
function arrayBufferToBase64(buffer) {
    let binary = '';
    const bytes = new Uint8Array(buffer);
    const len = bytes.byteLength;
    for (let i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
}

/**
 * Converts a Base64 string to an ArrayBuffer.
 * @param {string} base64 - The Base64 string to convert.
 * @returns {ArrayBuffer} - The resulting ArrayBuffer.
 */
function base64ToArrayBuffer(base64) {
    const binary = window.atob(base64);
    const len = binary.length;
    const bytes = new Uint8Array(len);
    for (let i = 0; i < len; i++) {
        bytes[i] = binary.charCodeAt(i);
    }
    return bytes.buffer;
}

// Expose functions globally
window.decryptSecret = decryptSecret;
window.getEncryptedSecret = getEncryptedSecret;
