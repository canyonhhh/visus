# This file is auto-generated from the current state of the database. Instead
# of editing this file, please use the migrations feature of Active Record to
# incrementally modify your database, and then regenerate this schema definition.
#
# This file is the source Rails uses to define your schema when running `bin/rails
# db:schema:load`. When creating a new database, `bin/rails db:schema:load` tends to
# be faster and is potentially less error prone than running all of your
# migrations from scratch. Old migrations may fail to apply correctly if those
# migrations use external dependencies or application code.
#
# It's strongly recommended that you check this file into your version control system.

ActiveRecord::Schema[8.1].define(version: 2025_11_11_200409) do
  create_table "activities", force: :cascade do |t|
    t.integer "business_id", null: false
    t.datetime "created_at", null: false
    t.text "description"
    t.boolean "is_active", default: true
    t.string "name"
    t.datetime "updated_at", null: false
    t.index ["business_id"], name: "index_activities_on_business_id"
  end

  create_table "attendance_records", force: :cascade do |t|
    t.integer "activity_id", null: false
    t.datetime "created_at", null: false
    t.integer "student_id", null: false
    t.datetime "updated_at", null: false
    t.index ["activity_id"], name: "index_attendance_records_on_activity_id"
    t.index ["student_id"], name: "index_attendance_records_on_student_id"
  end

  create_table "businesses", force: :cascade do |t|
    t.string "contact_info"
    t.datetime "created_at", null: false
    t.string "name"
    t.datetime "updated_at", null: false
  end

  create_table "device_fingerprints", force: :cascade do |t|
    t.datetime "created_at", null: false
    t.string "fingerprint_value"
    t.datetime "last_seen_at"
    t.integer "student_id", null: false
    t.datetime "updated_at", null: false
    t.index ["fingerprint_value"], name: "index_device_fingerprints_on_fingerprint_value", unique: true
    t.index ["student_id"], name: "index_device_fingerprints_on_student_id"
  end

  create_table "employees", force: :cascade do |t|
    t.integer "business_id"
    t.datetime "created_at", null: false
    t.string "email"
    t.string "name"
    t.boolean "onboarded", default: false, null: false
    t.string "password_digest"
    t.integer "role", default: 0, null: false
    t.datetime "updated_at", null: false
    t.index ["business_id"], name: "index_employees_on_business_id"
  end

  create_table "qr_codes", force: :cascade do |t|
    t.integer "activity_id", null: false
    t.datetime "created_at", null: false
    t.boolean "is_active", default: true
    t.string "token"
    t.datetime "updated_at", null: false
    t.index ["activity_id"], name: "index_qr_codes_on_activity_id"
    t.index ["token"], name: "index_qr_codes_on_token", unique: true
  end

  create_table "students", force: :cascade do |t|
    t.datetime "created_at", null: false
    t.string "full_name"
    t.datetime "updated_at", null: false
  end

  add_foreign_key "activities", "businesses"
  add_foreign_key "attendance_records", "activities"
  add_foreign_key "attendance_records", "students"
  add_foreign_key "device_fingerprints", "students"
  add_foreign_key "employees", "businesses"
  add_foreign_key "qr_codes", "activities"
end
